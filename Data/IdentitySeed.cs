using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RestauranteAPP_TP3.Models;
using System;
using System.Threading.Tasks;

namespace RestauranteAPP_TP3.Data
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();

            string[] roleNames = { "Administrador", "Comprador", "Aprovador", "Fornecedor" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Optional: create a default admin user (change password / move to secrets in real apps)
            var adminEmail = "admin@example.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                var adminUser = new Usuario
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Nome = "Administrador"
                };
                var result = await userManager.CreateAsync(adminUser, "Admin#2025!"); // use secure secret in production
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrador");
                }
            }
        }
    }
}
