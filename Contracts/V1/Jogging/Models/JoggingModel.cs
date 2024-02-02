using System.ComponentModel.DataAnnotations;

namespace Contracts.V1.Jogging.Models
{
    public class JoggingModel
    {
        /// <summary>
        /// Jogging Date
        /// </summary>
        [Required]
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Jogging Distance
        /// </summary>
        [Required]
        public double Distance { get; set; }

        /// <summary>
        /// Jogging Time
        /// </summary>
        [Required]
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Jogging Location
        /// </summary>
        [Required]
        public string Location { get; set; } = default!;
    }
}
