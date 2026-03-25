using BarberLegacy.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace BarberLegacy.Api.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            // Traemos el administrador de roles de Identity
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Los 3 roles que necesita tu barbería
            string[] roleNames = { "Admin", "Barber", "Client" };

            foreach (var roleName in roleNames)
            {
                // Preguntamos si el rol ya existe en la tabla AspNetRoles
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                // Si no existe, lo creamos
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminEmail = "admin@barberlegacy.com";

            // Nos fijamos si el adm ya existe en la base de datos
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                var result = await userManager.CreateAsync(newAdmin, "Password123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    
    }
}
