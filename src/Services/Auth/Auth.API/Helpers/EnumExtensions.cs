using System.ComponentModel;
using System.Reflection;

namespace Auth.API.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var member = type.GetMember(value.ToString());
            if (member.Length > 0)
            {
                var attr = member[0].GetCustomAttribute<DescriptionAttribute>();
                if (attr != null)
                    return attr.Description;
            }
            return value.ToString();
        }
    }
}
