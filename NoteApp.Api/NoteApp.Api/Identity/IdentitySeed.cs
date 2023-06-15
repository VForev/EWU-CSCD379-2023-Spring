using Microsoft.AspNetCore.Identity;
using NoteApp.Api.Data;

namespace NoteApp.Api.Identity;

public static class IdentitySeed
{
    public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Seed Roles
        await SeedRolesAsync(roleManager);

        // Seed Admin User
        await SeedAdminUserAsync(userManager);

        // Seed Standard User
        await SeedStandardUserAsync(userManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        // Seed Roles
        if (!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        }
        if (!await roleManager.RoleExistsAsync(Roles.Standard))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Standard));
        }
    }

    private static async Task SeedAdminUserAsync(UserManager<AppUser> userManager)
    {
        // Seed Admin User
        if (await userManager.FindByEmailAsync("admin@noteapp.com") == null)
        {
            var user = new AppUser { UserName = "admin@noteapp.com", Email = "admin@noteapp.com", Name = "Admin" };

            IdentityResult result = userManager.CreateAsync(user, "6EXWKa@U").Result;

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Admin);
                await userManager.AddToRoleAsync(user, Roles.Standard);
            }
            else if (result.Errors.Any())
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }

    private static async Task SeedStandardUserAsync(UserManager<AppUser> userManager)
    {
        // Seed Standard User => Hunter T
        if (await userManager.FindByEmailAsync("hunter.t@noteapp.com") == null)
        {
            var user =
                new AppUser { UserName = "hunter.t@noteapp.com", Email = "hunter.t@noteapp.com", Name = "Hunter T" };

            IdentityResult result = userManager.CreateAsync(user, "2?i2rhDm").Result;

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Standard);
            }
            else if (result.Errors.Any())
            {
                throw new Exception(result.Errors.First().Description);
            }
        }

        // Seed Standard User => Nolan P
        if (await userManager.FindByEmailAsync("nposey@noteapp.com") == null)
        {
            var user = new AppUser { UserName = "nposey@noteapp.com", Email = "nposey@noteapp.com", Name = "Nolan P" };

            IdentityResult result = userManager.CreateAsync(user, "fch!cM3x").Result;

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Standard);
            }
            else if (result.Errors.Any())
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}
