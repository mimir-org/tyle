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
        Task<IEnumerable<AttributeConditionLibCm>> GetConditions();
        Task<IEnumerable<AttributeFormatLibCm>> GetFormats();
        Task<IEnumerable<AttributeQualifierLibCm>> GetQualifiers();
        Task<IEnumerable<AttributeSourceLibCm>> GetSources();
    }
}