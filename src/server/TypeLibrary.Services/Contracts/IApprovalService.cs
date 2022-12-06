using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IApprovalService
    {
        Task<ICollection<ApprovalCm>> GetApprovals();
    }
}
