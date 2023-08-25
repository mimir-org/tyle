/*using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Models.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Records;

public record ApprovalData
{
    private List<ApprovalCm> Blocks { get; } = new();
    private List<ApprovalCm> Terminals { get; } = new();
    private List<ApprovalCm> Attributes { get; } = new();
    private List<ApprovalCm> Units { get; } = new();
    private List<ApprovalCm> Rds { get; } = new();

    public ICollection<ApprovalCm> GetAllData()
    {
        var allData = new List<ApprovalCm>();
        allData.AddRange(Blocks);
        allData.AddRange(Terminals);
        allData.AddRange(Attributes);
        allData.AddRange(Units);
        allData.AddRange(Rds);
        return allData;
    }

    public Task ResolveBlocks(IBlockService blockService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = blockService.GetLatestVersions().Where(x => x.State == State.Review).ToList();
        data = data.Where(x => authService.HasAccess(x.CompanyId, State.Approved).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Blocks.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveTerminals(ITerminalService terminalService, IMapper mapper, IMimirorgAuthService authService)
    {
        if (!authService.HasAccess(CompanyConstants.AnyCompanyId, State.Approved).Result) return Task.CompletedTask;

        var data = terminalService.Get().Where(x => x.State == State.Review).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Terminals.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveAttributes(IAttributeService attributeService, IMapper mapper, IMimirorgAuthService authService)
    {
        if (!authService.HasAccess(CompanyConstants.AnyCompanyId, State.Approved).Result) return Task.CompletedTask;

        var data = attributeService.Get().Where(x => x.State == State.Review).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Attributes.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveUnits(IUnitService unitService, IMapper mapper, IMimirorgAuthService authService)
    {
        if (!authService.HasAccess(CompanyConstants.AnyCompanyId, State.Approved).Result) return Task.CompletedTask;

        var data = unitService.Get().Where(x => x.State == State.Review).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Units.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveRds(IRdsService rdsService, IMapper mapper, IMimirorgAuthService authService)
    {
        if (!authService.HasAccess(CompanyConstants.AnyCompanyId, State.Approved).Result) return Task.CompletedTask;

        var data = rdsService.Get().Where(x => x.State == State.Review).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Rds.AddRange(mappedData);
        return Task.CompletedTask;
    }
}*/