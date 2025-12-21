namespace ServiceDesk.Application.Features.Queues.Queries.Get;

public class QueueResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}
