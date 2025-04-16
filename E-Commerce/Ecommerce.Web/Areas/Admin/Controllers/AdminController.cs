using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Areas.Admin.Controllers;

public class AdminController(RoleManager<IdentityRole> _roleManager,UserManager<IdentityUser> userManager):Controller
{
    
    public static async Task MakeStaticSuperAdminAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        const string userName = "SuperAdmin";
        const string email = "supereadmin@gmail.com";
        const string passwordAdmin = "Admin**.123";
        const string rolAdmin = RoleType.Admin;
        
        if (!await roleManager.RoleExistsAsync(rolAdmin))
            await roleManager.CreateAsync(new IdentityRole(rolAdmin));
        
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new User
            {
                UserName = userName,
                Email = email,
                SessionId = Guid.NewGuid().ToString(),
                ImgProfile = "/img/useer2.png"
            };

            var resultado = await userManager.CreateAsync(user, passwordAdmin);
            if (resultado.Succeeded)
            {
                await userManager.AddToRoleAsync(user, rolAdmin);
            }
        }
    }


    public static async Task CreateRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = { "Admin", "Client" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}