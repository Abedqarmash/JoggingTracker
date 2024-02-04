using Contracts.V1.Jogging.Models;
using Contracts.V1.Jogging.Resources;
using DataAccess.SQL.Entities;

namespace BusinessLogic.JoggingTracker
{
    public static class JoggingMapper
    {
        public static JoggingTrackerResource ToResource(this JoggingEntity model)
        {
            return new()
            {
                Id = model.Id,
                Date = model.Date,
                Distance = model.Distance,
                Time = model.Time,
                Location = model.Location,
                UserId = model.UserId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                ModifiedBy = model.ModifiedBy,
                ModifiedOn = model.ModifiedOn,
            };
        }

        public static JoggingEntity ToEntity(this JoggingModel model)
        {
            return new()
            {
                Date = model.Date,
                Distance = model.Distance,
                Time = model.Time,
                Location = model.Location
            };
        }

        public static JoggingEntity MapEntityTrackableInformation(this JoggingEntity entity,
        string? createdBy = null,
        DateTimeOffset? createdOn = null,
        string? modifiedBy = null)
        {
            entity.CreatedBy = createdBy ?? entity.CreatedBy;
            entity.CreatedOn = createdOn ?? entity.CreatedOn;

            if (entity.Id != 0)
            {
                entity.ModifiedBy = modifiedBy;
                entity.ModifiedOn = DateTime.Now;
            }

            return entity;
        }
    }
}
