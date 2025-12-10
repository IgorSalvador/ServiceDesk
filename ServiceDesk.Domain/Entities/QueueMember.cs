namespace ServiceDesk.Domain.Entities;

public class QueueMember
{
    public Guid ServiceQueueId { get; private set; }
    public ServiceQueue ServiceQueue { get; private set; } = null!;

    public Guid UserId { get; private set; }
    public bool IsQueueManager { get; private set; }
    public DateTime JoinedAt { get; private set; }

    private QueueMember()
    {
    }

    public QueueMember(Guid serviceQueueId, Guid userId, bool isQueueManager = false)
    {
        ServiceQueueId = serviceQueueId;
        UserId = userId;
        IsQueueManager = isQueueManager;
        JoinedAt = DateTime.UtcNow;
    }

    public void PromoteToManager() => IsQueueManager = true;

    public void DemoteFromManager() => IsQueueManager = false;
}
