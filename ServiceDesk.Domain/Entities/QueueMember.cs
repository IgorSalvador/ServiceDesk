namespace ServiceDesk.Domain.Entities;

public class QueueMember
{
    public Guid ServiceQueueId { get; set; }
    public ServiceQueue ServiceQueue { get; set; } = null!;

    public Guid UserId { get; set; }
    public bool IsQueueManager { get; set; }
}
