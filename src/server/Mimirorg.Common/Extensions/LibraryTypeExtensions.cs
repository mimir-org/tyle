using System.Reflection;

namespace Mimirorg.Common.Extensions;

public static class LibraryTypeExtensions
{
    public static List<Type> GetImplementations(this Type service, List<Assembly> assemblies)
    {
        if (!assemblies.Any())
            return new List<Type>();

        if (service.IsGenericType && service.IsGenericTypeDefinition)
        {
            return assemblies.SelectMany(a => a.GetTypes())
                .Where(type => type.IsGenericType && type.IsClass && type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == service.GetGenericTypeDefinition())).ToList();
        }

        return assemblies.SelectMany(a => a.GetTypes())
            .Where(type => service.IsAssignableFrom(type) && type.IsClass).ToList();
    }
}