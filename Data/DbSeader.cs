using Microsoft.AspNetCore.Identity;

namespace BuildWeek2.Data
{
    public static class DbSeader
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.EnsureCreatedAsync();

            if (!await roleManager.RoleExistsAsync("Farmacista"))
            {
                var role = new IdentityRole
                {
                    Name = "Farmacista",
                    NormalizedName = "FARMACISTA"
                };
                await roleManager.CreateAsync(role);

            }

            var email = "farmacista@gmail.com";
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "farmacista1",
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
}
