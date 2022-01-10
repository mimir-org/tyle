using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeLibrary.Models.Extensions
{
    public static class TypeExtensions
    {
        public static List<Type> GetImplementations(this Type service, List<Assembly> assemblies)
        {
            if (assemblies == null || !assemblies.Any())
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
}
