using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeService
    {
        IEnumerable<AttributeLibCm> Get(Aspect aspect);
        Task<AttributeLibCm> Get(string id);
        Task Create(List<AttributeLibAm> attributes, bool createdBySystem = false);
        Task<AttributeLibCm> Create(AttributeLibAm attribute);

        IEnumerable<AttributePredefinedLibCm> GetPredefined();
        Task CreatePredefined(List<AttributePredefinedLibAm> predefined, bool createdBySystem = false);
        Task<IEnumerable<AttributeConditionLibCm>> GetConditions();
        Task<IEnumerable<AttributeFormatLibCm>> GetFormats();
        Task<IEnumerable<AttributeQualifierLibCm>> GetQualifiers();
        Task<IEnumerable<AttributeSourceLibCm>> GetSources();
        Task<IEnumerable<AttributeReferenceCm>> GetAttributeReferences();
    }
}