using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<ApplicationUser, 
        IdentityRole<Guid>, 
        Guid,
        IdentityUserClaim<Guid>,
        IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>(options)
{
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<ServiceQueue> ServiceQueues { get; set; }
    public DbSet<QueueMember> QueueMembers { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<ServiceSubcategory> ServiceSubcategories { get; set; }
    public DbSet<Workflow> Workflows { get; set; }
    public DbSet<WorkflowStep> WorkflowSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
