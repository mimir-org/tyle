namespace Tyle.Application.Common;

public interface IApprovalService
{
    Task<bool> RequestApprovalForAttribute(Guid id);

    Task<bool> RequestApprovalForTerminal(Guid id);

    Task<bool> RequestApprovalForBlock(Guid id);

    Task<bool> ApproveAttribute(Guid id);

    Task<bool> ApproveTerminal(Guid id);

    Task<bool> ApproveBlock(Guid id);
}
