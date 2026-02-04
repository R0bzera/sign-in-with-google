namespace Application.DTOs;

public record ExternalUserData(
    string Provider,
    string ProviderUserId,
    string Email,
    string Name
);
