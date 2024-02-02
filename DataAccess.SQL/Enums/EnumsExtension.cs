using System.ComponentModel;
using System.Reflection;

namespace DataAccess.SQL.Enums
{
    public static class EnumsExtension
    {
        public static string GetDescription(this Enum en)
        {
            MemberInfo[] member = en.GetType().GetMember(en.ToString());
            if (member.Length == 0)
            {
                return en.ToString();
            }

            object[] customAttributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
            if (customAttributes.Length == 0)
            {
                return en.ToString();
            }

            return ((DescriptionAttribute)customAttributes[0]).Description;
        }
    }
}
