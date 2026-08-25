using System.Reflection;

namespace CheckAndThrow;

internal static class EnumCache<T>
    where T : struct, Enum
{
    internal static readonly Dictionary<string, T> Values = typeof(T)
        .GetFields(BindingFlags.Static | BindingFlags.Public)
        .ToDictionary(x => x.Name, x => (T)x.GetValue(null)!);

    internal static readonly Dictionary<string, T> ValuesIgnoreCase = typeof(T)
        .GetFields(BindingFlags.Static | BindingFlags.Public)
        .ToDictionary(x => x.Name, x => (T)x.GetValue(null)!, StringComparer.OrdinalIgnoreCase);

    internal static readonly Type UnderlyingType = typeof(T).GetEnumUnderlyingType();
}
