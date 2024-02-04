namespace Contracts.V1.Jogging.Resources
{
    public class JoggingTrackerResource
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
        /// Jogging Location
        /// </summary>
        public string Location { get; set; } = default!;

        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; } = default!;

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
