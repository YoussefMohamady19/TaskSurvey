using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TaskSurvey.Models;

namespace TaskSurvey.Admin
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleExist = await roleManager.RoleExistsAsync("Admin");
            if (!roleExist)
            {
                // Create the "Admin" role if it does not exist
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var user = await userManager.FindByEmailAsync("admin@yourdomain.com");

            if (user == null)
            {
                // Create an admin user with a hashed password
                user = new Users
                {
                    UserName = "admin@yourdomain.com",
                    Email = "admin@yourdomain.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "AdminStrongPassword123!");
                if (result.Succeeded)
                {
                    // Assign the "Admin" role to the user
                    await userManager.AddToRoleAsync(user, "Admin");

                    // Optionally, you can add custom claims here (like SuperAdmin)
                    await userManager.AddClaimAsync(user, new Claim("SuperAdmin", "True"));
                }
            }
        }
    }
}
