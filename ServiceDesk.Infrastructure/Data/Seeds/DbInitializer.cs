using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Infrastructure.Data.Seeds;

public class DbInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IConfiguration _configuration;

    public DbInitializer(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task SeedAsync()
    {
        await CreateReoleIfNotExists("SystemAdmin");
        await CreateReoleIfNotExists("Manager");
        await CreateReoleIfNotExists("Technician");
        await CreateReoleIfNotExists("Requester");

        // Create a default System Admin user
        var adminEmail = "admin@helpdesk.com";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser("admin", adminEmail, "Administrador do Sistema");

            var userAdminPassword = _configuration["SystemSettings:AdminPassword"] ?? string.Empty;
            var result = await _userManager.CreateAsync(adminUser, userAdminPassword);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "SystemAdmin");
            }
        }
    }

    private async Task CreateReoleIfNotExists(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
    }
}
