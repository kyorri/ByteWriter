using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;
using ByteWriter.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ByteWriter.Services
{
    public class DataSeederService : IDataSeederService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRegisterService _registerService;

        public DataSeederService(IServiceProvider serviceProvider, IRegisterService registerService)
        {
            _serviceProvider = serviceProvider;
            _registerService = registerService;
        }

        public async Task SeedRolesAndAdminAsync()
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = _serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "User", "Administrator" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = new User
            {
                UserName = "administrator@bytewriter.com",
                Email = "administrator@bytewriter.com"
            };
    
            string adminPassword = "BytewriterRules123!";
            var user = await userManager.FindByEmailAsync("administrator@bytewriter.com");

            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await _registerService.CreateBlogForUserAsync(adminUser);
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }
        }
    }
}
