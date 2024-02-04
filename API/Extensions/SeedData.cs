using DataAccess.SQL.Entities;
using DataAccess.SQL.Enums;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions
{
    public static class SeedData
    {
        public static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in Enum.GetNames(typeof(RoleType)))
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

        }

        public static async Task Initialize(UserManager<UserEntity> userManager)
        {
            var adminUserModel = new UserEntity
            {
                UserName = "AdminUser",
                Email = "userAdmin@admin.com",
                CreatedBy = "System user",
                CreatedOn = DateTimeOffset.Now,
            };

            var adminUser = await userManager.FindByEmailAsync(adminUserModel.Email);
            if (adminUser is null)
            {
                var createdResult = await userManager.CreateAsync(adminUserModel, "AdminPass@123");

                if (createdResult.Succeeded)
                {
                    adminUserModel = await userManager.FindByEmailAsync(adminUserModel.Email);

                    await userManager.AddToRoleAsync(adminUserModel, RoleType.Admin.GetDescription());
                }

                await CreateUser(userManager, "UserManager", "userManager@mnager.com", RoleType.UserManager.GetDescription());
                await CreateUser(userManager, "User", "user@normalUser.com", RoleType.User.GetDescription());
            }
            else
            {
                await userManager.AddToRoleAsync(adminUser, RoleType.Admin.GetDescription());
            }
        }

        private static async Task CreateUser(UserManager<UserEntity> userManager, string username, string email, string role)
        {
            var model = new UserEntity
            {
                UserName = username,
                Email = email,
                CreatedBy = "System user",
                CreatedOn = DateTimeOffset.Now,
            };
            var createdResult = await userManager.CreateAsync(model, "AdminPass@123");

            if (createdResult.Succeeded)
            {
                model = await userManager.FindByEmailAsync(model.Email);

                await userManager.AddToRoleAsync(model, role);
            }
        }
    }
}
