using Tyle.Application.Attributes;
using Tyle.Application.Blocks;
using Tyle.Application.Terminals;
using Tyle.Core.Common;

namespace Tyle.Application.Common;

public class ApprovalService : IApprovalService
{
    private readonly IAttributeRepository _attributeRepository;
    private readonly ITerminalRepository _terminalRepository;
    private readonly IBlockRepository _blockRepository;

    public ApprovalService(IAttributeRepository attributeRepository, ITerminalRepository terminalRepository, IBlockRepository blockRepository)
    {
        _attributeRepository = attributeRepository;
        _terminalRepository = terminalRepository;
        _blockRepository = blockRepository;
    }

    public async Task<bool> RequestApprovalForAttribute(Guid id)
    {
        return await _attributeRepository.ChangeState(id, State.Review);
    }

    public async Task<bool> RequestApprovalForTerminal(Guid id)
    {
        return await _terminalRepository.ChangeState(id, State.Review);
    }

    public async Task<bool> RequestApprovalForBlock(Guid id)
    {
        return await _blockRepository.ChangeState(id, State.Approved);
    }

    public async Task<bool> ApproveAttribute(Guid id)
    {
        return await _attributeRepository.ChangeState(id, State.Approved);
    }

    public async Task<bool> ApproveTerminal(Guid id)
    {
        return await _terminalRepository.ChangeState(id, State.Approved);
    }

    public async Task<bool> ApproveBlock(Guid id)
    {
        return await _blockRepository.ChangeState(id, State.Approved);
    }
}
