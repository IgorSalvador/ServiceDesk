namespace ServiceDesk.Domain.Entities;

public class ServiceCategory : Entity
{
    public string Name { get; private set; }
    public ICollection<ServiceSubcategory> Subcategories { get; private set; }

    private ServiceCategory() : base()
    {
        Name = string.Empty;
        Subcategories = [];
    }

    public ServiceCategory(string name) : base()
    {
        Name = name;
        Subcategories = [];
    }
}
