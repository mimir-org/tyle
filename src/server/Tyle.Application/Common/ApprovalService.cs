using Tyle.Application.Attributes;
using Tyle.Application.Blocks;
using Tyle.Application.Common.Requests;
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

    public async Task<ApprovalResponse> ChangeAttributeState(Guid id, State state)
    {
        var attribute = await _attributeRepository.Get(id);

        if (attribute == null)
        {
            return ApprovalResponse.TypeNotFound;
        }

        if ((attribute.State == State.Draft && state != State.Review) || attribute.State == State.Approved)
        {
            return ApprovalResponse.IllegalChange;
        }

        return await _attributeRepository.ChangeState(id, state) ? ApprovalResponse.Accepted : ApprovalResponse.TypeNotFound;
    }

    public async Task<ApprovalResponse> ChangeTerminalState(Guid id, State state)
    {
        var terminal = await _terminalRepository.Get(id);

        if (terminal == null)
        {
            return ApprovalResponse.TypeNotFound;
        }

        if ((terminal.State == State.Draft && state != State.Review) || terminal.State == State.Approved)
        {
            return ApprovalResponse.IllegalChange;
        }

        return await _terminalRepository.ChangeState(id, state) ? ApprovalResponse.Accepted : ApprovalResponse.TypeNotFound;
    }

    public async Task<ApprovalResponse> ChangeBlockState(Guid id, State state)
    {
        var block = await _blockRepository.Get(id);

        if (block == null)
        {
            return ApprovalResponse.TypeNotFound;
        }

        if ((block.State == State.Draft && state != State.Review) || block.State == State.Approved)
        {
            return ApprovalResponse.IllegalChange;
        }

        return await _blockRepository.ChangeState(id, state) ? ApprovalResponse.Accepted : ApprovalResponse.TypeNotFound;
    }
}
