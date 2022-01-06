using System.Collections.Generic;
using System.Threading.Tasks;
using Mb.Models.Application.TypeEditor;
using Mb.Models.Data.TypeEditor;
using Mb.Models.Enums;

namespace TypeLibrary.Services.Contracts
{
    public interface ILibraryTypeService
    {
        Task<LibraryType> GetTypeById(string id, bool ignoreNotFound = false);
        IEnumerable<CreateLibraryType> GetAllTypes();
        Task<IEnumerable<LibraryType>> CreateLibraryTypes(ICollection<CreateLibraryType> createLibraryTypes);
        Task<T> CreateLibraryType<T>(CreateLibraryType createLibraryType) where T : class, new();
        Task<T> UpdateLibraryType<T>(string id, CreateLibraryType createLibraryType, bool updateMajorVersion, bool updateMinorVersion) where T : class, new();
        Task DeleteType(string id);
        Task<CreateLibraryType> ConvertToCreateLibraryType(string id, LibraryFilter filter);
        Task<SimpleType> CreateSimpleType(SimpleTypeAm simpleType);
        Task CreateSimpleTypes(ICollection<SimpleTypeAm> simpleTypes);
        IEnumerable<SimpleType> GetSimpleTypes();
        void ClearAllChangeTracker();
    }
}
