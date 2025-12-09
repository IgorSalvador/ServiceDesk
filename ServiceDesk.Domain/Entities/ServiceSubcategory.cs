namespace ServiceDesk.Domain.Entities
{
    public class ServiceSubcategory : Entity
    {
        public string Name { get; private set; }
        public Guid ServiceCategoryId { get; private set; }


        public Guid? DefaultWorkflowId { get; private set; }
        public Workflow DefaultWorkflow { get; private set; }

        public ServiceSubcategory(string name, Guid categoryId) : base()
        {
            Name = name;
            ServiceCategoryId = categoryId;
        }

        public void SetWorkflow(Guid workflowId) => DefaultWorkflowId = workflowId;
    }
}
