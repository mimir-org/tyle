using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IQualifierService
    {
        Task<IEnumerable<QualifierLibAm>> GetQualifiers();
        Task<QualifierLibAm> UpdateQualifier(QualifierLibAm dataAm);
        Task<QualifierLibAm> CreateQualifier(QualifierLibAm dataAm);
        Task CreateQualifiers(List<QualifierLibAm> dataAm);
    }
}