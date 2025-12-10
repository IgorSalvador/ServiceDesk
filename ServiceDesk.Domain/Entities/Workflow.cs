namespace ServiceDesk.Domain.Entities
{
    public class Workflow : Entity
    {
        public string Name { get; private set; }
        public int Version { get; private set; }
        public ICollection<WorkflowStep> Steps { get; private set; }

        private Workflow() : base()
        {
            Name = string.Empty;
            Version = 1;
            Steps = [];
        }

        public Workflow(string name) : base()
        {
            Name = name;
            Version = 1;
            Steps = [];
        }
    }
}
