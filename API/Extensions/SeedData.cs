using DataAccess.SQL.Entities;
using DataAccess.SQL.Enums;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<UserEntity> userManager)
        {
            var adminUserModel = new UserEntity
            {
                UserName = "AdminUser",
                Email = "userAdmin@admin.com",
                CreatedBy = "System user",
                CreatedOn = DateTimeOffset.Now,
            };
            var createdResult = await userManager.CreateAsync(adminUserModel,"AdminPass@123");

            if(createdResult.Succeeded)
            {
                adminUserModel = await userManager.FindByEmailAsync(adminUserModel.Email);

                foreach (var roleName in Enum.GetNames(typeof(RoleType)))
                {
                    await userManager.AddToRoleAsync(adminUserModel, roleName);
                }
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
    }
}
