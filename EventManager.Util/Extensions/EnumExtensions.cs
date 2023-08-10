using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Util.Extensions
{
    public static class EnumExtensions
    {
        public static string? GetDisplayName(this Enum enumValue)
        {

            MemberInfo[] memberInfo = enumValue.GetType().GetMember(enumValue.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DisplayAttribute)attrs[0]).Name;
                }
            }

            return enumValue.ToString();
        }
    }
}
