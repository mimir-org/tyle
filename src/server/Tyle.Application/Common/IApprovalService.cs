using Tyle.Application.Common.Requests;

namespace Tyle.Application.Common;

public interface IApprovalService
{
    Task<ApprovalResponse> ChangeAttributeState(Guid id, ApprovalRequest request);

    Task<ApprovalResponse> ChangeTerminalState(Guid id, ApprovalRequest request);

    Task<ApprovalResponse> ChangeBlockState(Guid id, ApprovalRequest request);
}
