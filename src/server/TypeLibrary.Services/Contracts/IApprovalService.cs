using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IApprovalService
    {
        /// <summary>
        /// Get approvals from database
        /// </summary>
        /// <returns>A collection of approvals</returns>
        Task<ICollection<ApprovalCm>> GetApprovals();
    }
}