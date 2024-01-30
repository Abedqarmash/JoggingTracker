using System.ComponentModel.DataAnnotations;

namespace DataAccess.SQL.Entities
{
    public class JoggingEntity
    {
        /// <summary>
        /// Jogging Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Jogging Date
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Jogging Distance
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// Jogging Time
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// User Id
        /// Foreign key for user
        /// </summary>
        [Required]
        public int UserId { get; set; }
        public UserEntity User { get; set; } = default!;


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
