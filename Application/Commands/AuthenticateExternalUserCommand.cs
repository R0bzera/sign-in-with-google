using MediatR;

namespace Application.Commands;

public record AuthenticateExternalUserCommand(string Provider, string Token) : IRequest<string>;
