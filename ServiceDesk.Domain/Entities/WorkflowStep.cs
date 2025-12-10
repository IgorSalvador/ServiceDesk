namespace ServiceDesk.Domain.Entities;

public class WorkflowStep : Entity
{
    public Guid WorkflowId { get; private set; }

    // Configuração da Etapa
    public string StepName { get; private set; } // Ex: "Aprovação Financeira"
    public int OrderIndex { get; private set; } // 1, 2, 3... (Define a linearidade)

    // Definição de Comportamento
    public int SLAInHours { get; private set; } // Requisito: SLA conta por parte do fluxo
    public Guid TargetQueueId { get; private set; } // Ao entrar aqui, vai para qual fila?
    public ServiceQueue TargetQueue { get; private set; } = null!;

    private WorkflowStep() : base()
    {
        StepName = string.Empty;
    }

    public WorkflowStep(Guid workflowId, string stepName, int orderIndex, int slaInHours, Guid targetQueueId) : base()
    {
        WorkflowId = workflowId;
        StepName = stepName;
        OrderIndex = orderIndex;
        SLAInHours = slaInHours;
        TargetQueueId = targetQueueId;
    }
}
