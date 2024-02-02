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

                // var model = new UserEntity
                //{
                //    UserName = "UserManager",
                //    Email = "userManager@userManager.com",
                //    CreatedBy = "System user",
                //    CreatedOn = DateTimeOffset.Now,
                //};
                // createdResult = await userManager.CreateAsync(model, "AdminPass@123");

                //if (createdResult.Succeeded)
                //{
                //    model = await userManager.FindByEmailAsync(model.Email);

                //    await userManager.AddToRoleAsync(model, "UserManager");
                //}

                //model = new UserEntity
                //{
                //    UserName = "User",
                //    Email = "user@user.com",
                //    CreatedBy = "System user",
                //    CreatedOn = DateTimeOffset.Now,
                //};
                //createdResult = await userManager.CreateAsync(model, "AdminPass@123");

                //if (createdResult.Succeeded)
                //{
                //    model = await userManager.FindByEmailAsync(model.Email);

                //    await userManager.AddToRoleAsync(model, "User");
                //}
            }
            else
            {
                await userManager.AddToRoleAsync(adminUser, RoleType.Admin.GetDescription());
            }
        }
    }
}
