using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.V1.SharedModels
{
    public class ResourceCollection<TResource>
    {
        /// <summary>
        /// Retreives items, counts with the total execuation time of the query.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="totalResults"></param>
        /// <param name="elapsedMilliseconds"></param>
        public ResourceCollection(IEnumerable<TResource> items, int totalResults)
        {
            Items = items;
            TotalResults = totalResults;
        }

        public ResourceCollection(IEnumerable<TResource> items, int totalResults, long elapsedMilliseconds)
        {
            Items = items;
            TotalResults = totalResults;
            ElapsedMilliseconds = elapsedMilliseconds;
        }

        /// <summary>
        /// Collection of items
        /// </summary>
        public IEnumerable<TResource> Items { get; set; }

        /// <summary>
        /// Number Of items
        /// </summary>
        public int TotalResults { get; set; }

        /// <summary>
        /// Elapsed time of the query.
        /// </summary>
        public long ElapsedMilliseconds { get; set; }
    }
}
