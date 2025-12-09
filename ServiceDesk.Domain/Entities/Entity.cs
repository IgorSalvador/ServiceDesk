namespace ServiceDesk.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public bool IsActive { get; protected set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;
}
