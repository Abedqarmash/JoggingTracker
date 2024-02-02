using System.ComponentModel.DataAnnotations;

namespace Contracts.V1.SharedModels
{
    /// <summary>
    /// Filter.
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// Skip Items.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int? Skip { get; set; }

        /// <summary>
        /// Take Items.
        /// </summary>
        [Range(1, 1000)]
        public int? Take { get; set; }
    }
}
