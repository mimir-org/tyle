using Tyle.Core.Common;

namespace Tyle.Application.Common;

public interface IApprovalService
{
    Task<ApprovalResponse> ChangeAttributeState(Guid id, State state);

    Task<ApprovalResponse> ChangeTerminalState(Guid id, State state);

    Task<ApprovalResponse> ChangeBlockState(Guid id, State state);
}
