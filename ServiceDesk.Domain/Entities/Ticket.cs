using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Domain.Entities;

public class Ticket : Entity
{
    public string Protocol { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TicketPriority Priority { get; private set; }
    public TicketStatus Status { get; private set; }

    public Guid RequesterId { get; private set; }

    public Guid? CurrentWorkflowId { get; private set; }

    public Guid? CurrentStepId { get; private set; }
    public virtual WorkflowStep? CurrentStep { get; private set; }

    public Guid? CurrentQueueId { get; private set; }
    public virtual ServiceQueue? CurrentQueue { get; private set; }

    public Guid? AssignedUserId { get; private set; }

    public DateTime CurrentStepEntryDate { get; private set; }
    public DateTime? CurrentStepDeadline { get; private set; }

    public Ticket() { }

    public Ticket(string title, string description, Guid requesterId, TicketPriority priority)
    {
        Title = title;
        Description = description;
        RequesterId = requesterId;
        Priority = priority;

        Status = TicketStatus.New;
        Protocol = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(100, 999).ToString();
    }
    public void StartWorkflow(Workflow workflow, WorkflowStep firstStep)
    {
        CurrentWorkflowId = workflow.Id;
        MoveToStep(firstStep);
    }

    public void MoveToStep(WorkflowStep nextStep)
    {
        CurrentStepId = nextStep.Id;
        CurrentQueueId = nextStep.TargetQueueId;
        CurrentStepEntryDate = DateTime.UtcNow;

        // Calcula o deadline
        CurrentStepDeadline = DateTime.UtcNow.AddHours(nextStep.SLAInHours);

        AssignedUserId = null;

        Status = TicketStatus.InProgress;
    }

    public void Resolve()
    {
        Status = TicketStatus.Resolved;
        CurrentStepDeadline = null;
    }

    // Método extra útil para self-assign
    public void AssignToUser(Guid userId)
    {
        AssignedUserId = userId;
        // Lógica adicional se necessário (ex: logar histórico)
    }
}
