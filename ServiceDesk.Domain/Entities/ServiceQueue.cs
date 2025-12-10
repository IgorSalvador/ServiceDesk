namespace ServiceDesk.Domain.Entities;

public class ServiceQueue : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ICollection<QueueMember> Members { get; private set; }

    private ServiceQueue() : base()
    {
        Name = string.Empty;
        Description = string.Empty;
        Members = [];
    }

    public ServiceQueue(string name, string description) : base()
    {
        Name = name;
        Description = description;
        Members = [];
    }
}
