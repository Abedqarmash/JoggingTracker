using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace DataAccess.SQL.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoleType
    {
        [Description("Admin")]
        Admin,

        [Description("UserManager")]
        UserManager,

        [Description("User")]
        User
    }
}
