using Contracts.V1.SharedModels;

namespace Contracts.V1.Jogging.Filters
{
    public class JoggingTrackerFilter : BaseFilter
    {
        /// <summary>
        /// Filter based on ids
        /// </summary>
        public IEnumerable<int> Ids { get; set; } = new List<int>();
    }
}
