using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeService
    {
        IEnumerable<AttributeLibCm> GetAll(Aspect aspect, bool includeDeleted = false);
        Task<AttributeLibCm> Get(string id);
        Task Create(List<AttributeLibAm> attributes, bool createdBySystem = false);
        Task<AttributeLibCm> Create(AttributeLibAm attribute, bool resetVersion = false);

        Task<IEnumerable<AttributeLibCm>> GetLatestVersions(Aspect aspect);
        Task<AttributeLibCm> Update(AttributeLibAm dataAm, string id);
        Task<AttributeLibCm> UpdateState(string id, State state);
        Task<bool> Delete(string id);
        Task<bool> CompanyIsChanged(string nodeId, int companyId);

        IEnumerable<AttributePredefinedLibCm> GetPredefined();
        Task CreatePredefined(List<AttributePredefinedLibAm> predefined, bool createdBySystem = false);
        Task<IEnumerable<AttributeConditionLibCm>> GetConditions();
        Task<IEnumerable<AttributeFormatLibCm>> GetFormats();
        Task<IEnumerable<AttributeQualifierLibCm>> GetQualifiers();
        Task<IEnumerable<AttributeSourceLibCm>> GetSources();
        Task<IEnumerable<TypeReferenceCm>> GetAttributeReferences();
    }
}