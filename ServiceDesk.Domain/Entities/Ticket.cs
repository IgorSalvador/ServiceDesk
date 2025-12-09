using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Domain.Entities;

public class Ticket : Entity
{
    public string Protocol { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TicketPriority Priority { get; private set; }
    public TicketStatus Status { get; private set; }

    // Solicitante
    public Guid RequesterId { get; private set; }

    // --- MÁQUINA DE ESTADOS (Onde o ticket está) ---

    public Guid? CurrentWorkflowId { get; private set; }

    public Guid? CurrentStepId { get; private set; }
    public WorkflowStep CurrentStep { get; private set; }

    public Guid CurrentQueueId { get; private set; }
    public ServiceQueue CurrentQueue { get; private set; }

    // Quem pegou o ticket para trabalhar? (Pode ser null se estiver solto na fila)
    public Guid? AssignedUserId { get; private set; }

    // --- CONTROLE DE SLA (Por Etapa) ---

    public DateTime CurrentStepEntryDate { get; private set; } // Quando entrou nesta etapa
    public DateTime? CurrentStepDeadline { get; private set; } // Quando estoura o SLA dessa etapa

    // Construtor para Abertura de Chamado
    public Ticket(string title, string description, Guid requesterId, TicketPriority priority)
    {
        Title = title;
        Description = description;
        RequesterId = requesterId;
        Priority = priority;
        Status = TicketStatus.New;
        Protocol = DateTime.Now.ToString("yyyyMMddHHmmss"); // Gerador simples por enquanto
    }

    // Método de Domínio: Inicializar o Workflow
    public void StartWorkflow(Workflow workflow, WorkflowStep firstStep)
    {
        CurrentWorkflowId = workflow.Id;
        MoveToStep(firstStep);
    }

    // Método de Domínio: Avançar Etapa
    public void MoveToStep(WorkflowStep nextStep)
    {
        CurrentStepId = nextStep.Id;
        CurrentQueueId = nextStep.TargetQueueId; // O Ticket muda de fila automaticamente!
        CurrentStepEntryDate = DateTime.UtcNow;

        // Calcula o deadline baseado na configuração do passo
        CurrentStepDeadline = DateTime.UtcNow.AddHours(nextStep.SLAInHours);

        // Reseta o técnico atribuído, pois mudou de fila/departamento
        AssignedUserId = null;

        Status = TicketStatus.InProgress;
    }

    // Método de Domínio: Resolver Ticket
    public void Resolve()
    {
        Status = TicketStatus.Resolved;
        CurrentStepDeadline = null; // Para o relógio
    }
}
