using Application.DTOs;
using Application.Interfaces;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace Infrastructure.OAuth;

public class GoogleAuthProvider : IExternalAuthProvider
{
    private readonly GoogleAuthSettings _settings;

    public GoogleAuthProvider(IOptions<GoogleAuthSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<ExternalUserData> ValidateTokenAsync(string token)
    {
        var validationSettings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { _settings.ClientId }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(token, validationSettings);

        if (!payload.EmailVerified)
        {
            throw new InvalidOperationException("Email not verified.");
        }

        return new ExternalUserData(
            Provider: "Google",
            ProviderUserId: payload.Subject,
            Email: payload.Email,
            Name: payload.Name
        );
    }
}
