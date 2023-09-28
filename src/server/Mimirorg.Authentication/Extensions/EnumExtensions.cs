using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mimirorg.Authentication.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// A generic extension method that aids in reflecting and retrieving any attribute that is applied to an Enum
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
    {
        return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<TAttribute>();
    }

    /// <summary>
    /// Get the name value of the DisplayAttribute to an Enum
    /// </summary>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetAttribute<DisplayAttribute>().GetName();
    }

    /// <summary>
    /// Convert enums to enum flag
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="flags"></param>
    /// <returns></returns>
    public static T ConvertToFlag<T>(this IEnumerable<T> flags) where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            throw new NotSupportedException($"{typeof(T)} must be an enumerated type");

        return (T) (object) flags.Cast<int>().Aggregate(0, (c, n) => c | n);
    }

    /// <summary>
    /// Convert enum to enumerable of T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<T> AsEnumerable<T>()
    {
        if (typeof(T).IsSubclassOf(typeof(Enum)) == false)
            throw new ArgumentException();

        return Enum.GetValues(typeof(T))
            .Cast<Enum>()
            .Cast<T>();
    }
}