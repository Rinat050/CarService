using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CarService.Extensions
{
    public static class EnumEextension
    {
        public static string? GetDisplayName(this Enum type)
        {
            return type.GetType().GetField(type.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name;
        }
    }
}
