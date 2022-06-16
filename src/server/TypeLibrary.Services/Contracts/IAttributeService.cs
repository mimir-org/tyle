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
        Task Create(List<AttributeLibAm> attributes, bool createdBySystem = false);

        IEnumerable<AttributePredefinedLibCm> GetPredefined();
        Task CreatePredefined(List<AttributePredefinedLibAm> predefined, bool createdBySystem = false);

        Task<IEnumerable<AttributeAspectLibCm>> GetAspects();
        Task CreateAspects(List<AttributeAspectLibAm> aspects, bool createdBySystem = false);

        Task<IEnumerable<AttributeConditionLibCm>> GetConditions();
        Task CreateConditions(List<AttributeConditionLibAm> conditions, bool createdBySystem = false);

        Task<IEnumerable<AttributeFormatLibCm>> GetFormats();
        Task CreateFormats(List<AttributeFormatLibAm> formats, bool createdBySystem = false);

        Task<IEnumerable<AttributeQualifierLibCm>> GetQualifiers();
        Task CreateQualifiers(List<AttributeQualifierLibAm> qualifiers, bool createdBySystem = false);

        Task<IEnumerable<AttributeSourceLibCm>> GetSources();
        Task CreateSources(List<AttributeSourceLibAm> sources, bool createdBySystem = false);
    }
}