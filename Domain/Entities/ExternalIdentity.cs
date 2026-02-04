namespace Domain.Entities;

public class ExternalIdentity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Provider { get; private set; }
    public string ProviderUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private ExternalIdentity() { }

    public ExternalIdentity(Guid id, Guid userId, string provider, string providerUserId, DateTime createdAt)
    {
        Id = id;
        UserId = userId;
        Provider = provider;
        ProviderUserId = providerUserId;
        CreatedAt = createdAt;
    }
}
