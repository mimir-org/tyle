/*using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;
using TypeLibrary.Services.Records;

namespace TypeLibrary.Services.Services;

public class ApprovalService : IApprovalService
{
    private readonly IMapper _mapper;
    private readonly IBlockService _blockService;
    private readonly ITerminalService _terminalService;
    private readonly IAttributeService _attributeService;
    private readonly IUnitService _unitService;
    private readonly IRdsService _rdsService;
    private readonly IMimirorgAuthService _authService;

    public ApprovalService(IMapper mapper, IBlockService blockService, ITerminalService terminalService, IAttributeService attributeService, IUnitService unitService, IRdsService rdsService, IMimirorgAuthService authService)
    {
        _mapper = mapper;
        _blockService = blockService;
        _terminalService = terminalService;
        _attributeService = attributeService;
        _unitService = unitService;
        _rdsService = rdsService;
        _authService = authService;
    }

    /// <summary>
    /// Get approvals from database
    /// </summary>
    /// <returns>A collection of approvals</returns>
    public async Task<ICollection<ApprovalCm>> GetApprovals()
    {
        var data = new ApprovalData();

        var tasks = new List<Task>
        {
            Task.Run(() => data.ResolveBlocks(_blockService, _mapper, _authService)),
            Task.Run(() => data.ResolveTerminals(_terminalService, _mapper, _authService)),
            Task.Run(() => data.ResolveAttributes(_attributeService, _mapper, _authService)),
            Task.Run(() => data.ResolveUnits(_unitService, _mapper, _authService)),
            Task.Run(() => data.ResolveRds(_rdsService, _mapper, _authService))
        };

        await Task.WhenAll(tasks);
        return data.GetAllData();
    }
}*/