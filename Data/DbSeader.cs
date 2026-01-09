using BuildWeek2.Data;
using Microsoft.AspNetCore.Identity;

public static class DbSeader
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync("Farmacista"))
            await roleManager.CreateAsync(new IdentityRole("Farmacista"));

        var email = "farmacista@gmail.com";

        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                NomeCompleto = "Mario Rossi",
                DataNascita = new DateTime(1980, 1, 1),
                CodiceFiscale = "RSSMRA80A01H501U",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, "Farmacista123!");
            await userManager.AddToRoleAsync(user, "Farmacista");
        }
    }
}
