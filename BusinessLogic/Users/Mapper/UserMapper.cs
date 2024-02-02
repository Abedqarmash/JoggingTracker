using Contracts.V1.User.Models;
using Contracts.V1.User.Resources;
using DataAccess.SQL.Entities;

namespace BusinessLogic.Users.Mapper
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(this RegisterModel model)
        {
            return new()
            {
                Email = model.Email,
                UserName = model.UserName
            };
        }

        public static UserResource ToResource(this UserEntity entity,
            IEnumerable<string>? roles = null)
        {
            return new()
            {
                Email = entity.Email,
                UserName = entity.UserName,
                roles = roles ?? Enumerable.Empty<string>()
            };
        }

        public static UserEntity MapEntityTrackableInformation(this UserEntity entity,
        string? createdBy = null,
        DateTimeOffset? createdOn = null,
        string? modifiedBy = null)
        {
            entity.CreatedBy = createdBy ?? entity.CreatedBy;
            entity.CreatedOn = createdOn ?? entity.CreatedOn;

            if (!string.IsNullOrEmpty(entity.Id))
            {
                entity.ModifiedBy = modifiedBy;
                entity.ModifiedOn = DateTime.Now;
            }

            return entity;
        }
    }
}
