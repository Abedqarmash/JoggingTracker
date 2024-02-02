using Contracts.V1.SharedModels;

namespace Contracts.V1.User.Filters
{
    public class UserListFilter : BaseFilter
    {
        /// <summary>
        /// User email
        /// </summary>
        public List<string> Emails { get; set; } = new List<string>();
    }
}
