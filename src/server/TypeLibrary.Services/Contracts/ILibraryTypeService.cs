using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Services.Contracts
{
    public interface ILibraryTypeService
    {
        Task<LibraryType> GetLibraryTypeById(string id, bool ignoreNotFound = false);
        IEnumerable<CreateLibraryType> GetAllLibraryTypes();
        Task<IEnumerable<LibraryType>> CreateLibraryTypes(ICollection<CreateLibraryType> createLibraryTypes);
        Task<T> CreateLibraryType<T>(CreateLibraryType createLibraryType) where T : class, new();
        Task<T> UpdateLibraryType<T>(string id, CreateLibraryType createLibraryType, bool updateMajorVersion, bool updateMinorVersion) where T : class, new();
        Task DeleteLibraryType(string id);
        Task<CreateLibraryType> ConvertToCreateLibraryType(string id, LibraryFilter filter);
        Task<SimpleType> CreateSimpleType(SimpleTypeAm simpleType);
        Task CreateSimpleTypes(ICollection<SimpleTypeAm> simpleTypes);
        IEnumerable<SimpleType> GetSimpleTypes();
        void ClearAllChangeTracker();

        Task<Library> GetLibraryTypes(string searchString);
        Task<IEnumerable<LibraryNodeItem>> GetNodeTypes();
        Task<IEnumerable<LibraryTransportItem>> GetTransportTypes();
        Task<IEnumerable<LibraryInterfaceItem>> GetInterfaceTypes();
        Task<IEnumerable<LibrarySubProjectItem>> GetSubProjects(string searchString = null);

    }
}