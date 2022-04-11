using System.Diagnostics.Metrics;
using Mimirorg.TypeLibrary.Contracts;

namespace Mimirorg.TypeLibrary.Extensions
{
    public static class LibraryExtensions
    {
        public static IEnumerable<T> Convert<T>(ICollection<string> idList) where T : ILibraryType, new()
        {
            if (idList == null || !idList.Any())
                yield break;

            foreach (var id in idList)
                yield return new T
                {
                    Id = id
                };
        }

        public static IEnumerable<string> Convert<T>(ICollection<T> objects) where T : ILibraryType, new()
        {
            if (objects == null || !objects.Any())
                yield break;

            foreach (var obj in objects)
                yield return obj.Id;
        }
    }
}
