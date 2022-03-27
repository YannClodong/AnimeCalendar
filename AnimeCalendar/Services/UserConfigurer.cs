using AnimeCalendar.Data;
using Microsoft.AspNetCore.Identity;

namespace AnimeCalendar.Services;

public class UserConfigurer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public UserConfigurer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        // ExecuteAsync(CancellationToken.None);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        
        var admin = await userManager.FindByEmailAsync(Secret.AdminEmail);
        if (admin == null)
        {
            admin = new User()
            {
                UserName = Secret.AdminEmail,
                Email = Secret.AdminEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(admin, Secret.AdminPassword);

            if (!result.Succeeded) return;
            
            await userManager.AddToRoleAsync(admin, ConstRoles.AdminRole);
            await userManager.AddToRoleAsync(admin, ConstRoles.AnimeEditor);
        }
    }
}