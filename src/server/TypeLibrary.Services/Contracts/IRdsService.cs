using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Application.TypeEditor;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Services.Contracts
{
    public interface IRdsService
    {
        IEnumerable<Rds> GetRds(Aspect aspect);
        Task<Rds> CreateRds(CreateRds createRds);
        Task<List<Rds>> CreateRdsAsync(List<CreateRds> createRds);
    }
}
