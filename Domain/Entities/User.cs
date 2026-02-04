namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User() { }

    public User(Guid id, string email, string name, bool isActive, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Name = name;
        IsActive = isActive;
        CreatedAt = createdAt;
    }
}
