using Microsoft.AspNetCore.Identity;

namespace ServiceDesk.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; private set; }
    public bool IsActive { get; private set; }

    // --- RELACIONAMENTOS DE NEGÓCIO ---

    // 1. Segurança: Em quais filas ele trabalha?
    public virtual ICollection<QueueMember> QueueMemberships { get; private set; }

    // 2. Operação: Quais tickets ele abriu?
    public virtual ICollection<Ticket> RequestedTickets { get; private set; }

    // 3. Operação: Quais tickets ele está atendendo?
    public virtual ICollection<Ticket> AssignedTickets { get; private set; }

    protected ApplicationUser()
    {
        FullName = string.Empty;

        QueueMemberships = [];
        RequestedTickets = [];
        AssignedTickets = [];
    }

    public ApplicationUser(string userName, string email, string fullName)
    {
        UserName = userName;
        Email = email;
        FullName = fullName;
        IsActive = true;
        SecurityStamp = Guid.NewGuid().ToString(); 

        QueueMemberships = [];
        RequestedTickets = [];
        AssignedTickets = [];
    }


    public void Deactivate() => IsActive = false;
}
