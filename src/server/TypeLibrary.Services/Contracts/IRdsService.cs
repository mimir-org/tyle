using System.Collections.Generic;
using System.Threading.Tasks;
using Mb.Models.Application.TypeEditor;
using Mb.Models.Data.TypeEditor;
using Mb.Models.Enums;

namespace TypeLibrary.Services.Contracts
{
    public interface IRdsService
    {
        IEnumerable<Rds> GetRds(Aspect aspect);
        Task<Rds> CreateRds(CreateRds createRds);
        Task<List<Rds>> CreateRdsAsync(List<CreateRds> createRds);
    }
}
