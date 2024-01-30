using Microsoft.AspNetCore.Identity;

namespace DataAccess.SQL.Entities
{
    public class UserEntity : IdentityUser<int>
    {
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

        /// <summary>
        /// Soft Delete flag
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
