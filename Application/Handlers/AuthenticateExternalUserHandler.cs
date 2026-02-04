using Application.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Handlers;

public class AuthenticateExternalUserHandler : IRequestHandler<AuthenticateExternalUserCommand, string>
{
    private readonly IExternalAuthProviderFactory _externalAuthProviderFactory;
    private readonly IExternalIdentityRepository _externalIdentityRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticateExternalUserHandler(
        IExternalAuthProviderFactory externalAuthProviderFactory,
        IExternalIdentityRepository externalIdentityRepository,
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _externalAuthProviderFactory = externalAuthProviderFactory;
        _externalIdentityRepository = externalIdentityRepository;
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> Handle(AuthenticateExternalUserCommand request, CancellationToken cancellationToken)
    {
        var externalAuthProvider = _externalAuthProviderFactory.GetProvider(request.Provider);
        var externalUserData = await externalAuthProvider.ValidateTokenAsync(request.Token);

        var externalIdentity = await _externalIdentityRepository.GetByProviderAndIdAsync(
            externalUserData.Provider,
            externalUserData.ProviderUserId);

        User user;

        if (externalIdentity is not null)
        {
            user = await _userRepository.GetByIdAsync(externalIdentity.UserId)
                ?? throw new InvalidOperationException("User not found.");
        }
        else
        {
            user = new User(
                id: Guid.NewGuid(),
                email: externalUserData.Email,
                name: externalUserData.Name,
                isActive: true,
                createdAt: DateTime.UtcNow);

            await _userRepository.AddAsync(user);

            var newExternalIdentity = new ExternalIdentity(
                id: Guid.NewGuid(),
                userId: user.Id,
                provider: externalUserData.Provider,
                providerUserId: externalUserData.ProviderUserId,
                createdAt: DateTime.UtcNow);

            await _externalIdentityRepository.AddAsync(newExternalIdentity);
        }

        var token = _jwtTokenGenerator.Generate(user);

        return token;
    }
}
