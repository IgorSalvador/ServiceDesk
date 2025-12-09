namespace ServiceDesk.Domain.Entities;

public class ServiceCategory : Entity
{
    public string Name { get; private set; }
    public ICollection<ServiceSubcategory> Subcategories { get; private set; }

    public ServiceCategory(string name)
    {
        Name = name;
        Subcategories = [];
    }
}
