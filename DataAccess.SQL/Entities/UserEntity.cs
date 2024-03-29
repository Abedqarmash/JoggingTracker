﻿using Microsoft.AspNetCore.Identity;

namespace DataAccess.SQL.Entities
{
    public class UserEntity : IdentityUser
    {
        /// <summary>
        /// Relation one to Many
        /// </summary>
        public List<JoggingEntity> joggingEntities { get; set; } = new List<JoggingEntity>();

        /// <summary>
        /// Created By.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Created On.
        /// </summary>
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Modified On
        /// </summary>
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
