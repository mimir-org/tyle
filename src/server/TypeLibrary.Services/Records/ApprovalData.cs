using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Models.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Records;

public record ApprovalData
{
    private List<ApprovalCm> AspectObjects { get; } = new();
    private List<ApprovalCm> Terminals { get; } = new();
    private List<ApprovalCm> Attributes { get; } = new();
    private List<ApprovalCm> Units { get; } = new();
    private List<ApprovalCm> QuantityDatums { get; } = new();
    private List<ApprovalCm> Rds { get; } = new();

    public ICollection<ApprovalCm> GetAllData()
    {
        var allData = new List<ApprovalCm>();
        allData.AddRange(AspectObjects);
        allData.AddRange(Terminals);
        allData.AddRange(Attributes);
        allData.AddRange(Units);
        allData.AddRange(QuantityDatums);
        allData.AddRange(Rds);
        return allData;
    }

    public Task ResolveAspectObjects(IAspectObjectService aspectObjectService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = aspectObjectService.GetLatestVersions().Where(x => x.State == State.Review).ToList();
        data = data.Where(x => authService.HasAccess(x.CompanyId, State.Approved).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        AspectObjects.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveTerminals(ITerminalService terminalService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = terminalService.Get().Where(x => x.State == State.Review).ToList();
        data = data.Where(x => authService.HasAccess(CompanyConstants.AnyCompanyId, State.Approved).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Terminals.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveAttributes(IAttributeService attributeService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = attributeService.Get().Where(x => x.State == State.Review).ToList();
        data = data.Where(x => authService.HasAccess(CompanyConstants.AnyCompanyId, State.Approved).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Attributes.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveUnits(IUnitService unitService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = unitService.Get().Where(x => x.State == State.Review).ToList();
        data = data.Where(x => authService.HasAccess(CompanyConstants.AnyCompanyId, State.Approved).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Units.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveQuantityDatums(IQuantityDatumService quantityDatumService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = quantityDatumService.Get().Where(x => x.State == State.Review).ToList();
        data = data.Where(x => authService.HasAccess(CompanyConstants.AnyCompanyId, State.Approved).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        QuantityDatums.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveRds(IRdsService rdsService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = rdsService.Get().Where(x => x.State == State.Review).ToList();
        data = data.Where(x => authService.HasAccess(CompanyConstants.AnyCompanyId, State.Approved).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Rds.AddRange(mappedData);
        return Task.CompletedTask;
    }
}