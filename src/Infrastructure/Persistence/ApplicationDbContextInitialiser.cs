using LogOT.Domain.Entities;
using LogOT.Domain.IdentityModel;
using LogOT.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LogOT.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // admin roles
       /* var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // staff roles
        var staffRole = new IdentityRole("Staff");

        if (_roleManager.Roles.All(r => r.Name != staffRole.Name))
        {
            await _roleManager.CreateAsync(staffRole);
        }

        // employee roles
        var employeeRole = new IdentityRole("Employee");

        if (_roleManager.Roles.All(r => r.Name != employeeRole.Name))
        {
            await _roleManager.CreateAsync(employeeRole);
        }

        // admin users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost", Fullname = "Administrator", Address="No", Image= "No" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }


        var staff = new ApplicationUser { UserName = "staff@localhost", Email = "staff@localhost" , Fullname = "Staff", Address = "No", Image = "No" };

        if (_userManager.Users.All(u => u.UserName != staff.UserName))
        {
            await _userManager.CreateAsync(staff, "StaffAccount1!");
            if (!string.IsNullOrWhiteSpace(staffRole.Name))
            {
                await _userManager.AddToRolesAsync(staff, new[] { staffRole.Name });
            }
        }*/

    }
}
