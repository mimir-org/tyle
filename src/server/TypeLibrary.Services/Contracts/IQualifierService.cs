using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IQualifierService
    {
        Task<IEnumerable<QualifierAm>> GetQualifiers();
        Task<QualifierAm> UpdateQualifier(QualifierAm dataAm);
        Task<QualifierAm> CreateQualifier(QualifierAm dataAm);
        Task CreateQualifiers(List<QualifierAm> dataAm);
    }
}