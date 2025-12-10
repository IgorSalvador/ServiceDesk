namespace ServiceDesk.Domain.Entities
{
    public class ServiceSubcategory : Entity
    {
        public string Name { get; private set; }
        public Guid ServiceCategoryId { get; private set; }
        public ServiceCategory ServiceCategory { get; private set; } = null!;

        public Guid? DefaultWorkflowId { get; private set; }
        public Workflow? DefaultWorkflow { get; private set; }

        // Construtor padrão vazio para o EF Core
        private ServiceSubcategory() : base()
        {
            Name = string.Empty;
        }

        // Construtor para criação da entidade
        public ServiceSubcategory(string name, Guid serviceCategoryId) : base()
        {
            Name = name;
            ServiceCategoryId = serviceCategoryId;
        }

        // Construtor completo com workflow
        public ServiceSubcategory(string name, Guid serviceCategoryId, Guid? defaultWorkflowId) : base()
        {
            Name = name;
            ServiceCategoryId = serviceCategoryId;
            DefaultWorkflowId = defaultWorkflowId;
        }
    }
}
