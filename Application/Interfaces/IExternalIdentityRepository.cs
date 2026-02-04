using Domain.Entities;

namespace Application.Interfaces;

public interface IExternalIdentityRepository
{
    Task<ExternalIdentity?> GetByProviderAndIdAsync(string provider, string providerUserId);
    Task AddAsync(ExternalIdentity externalIdentity);
}
