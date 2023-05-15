using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Constants;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Records;

public record ApprovalData()
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
        var data = aspectObjectService.GetLatestVersions().Where(x => x.State is State.Approve or State.Delete).ToList();
        data = data.Where(x => authService.HasAccess(x.CompanyId, NextStateMapper(x.State)).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        AspectObjects.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveTerminals(ITerminalService terminalService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = terminalService.Get().Where(x => x.State is State.Approve or State.Delete).ToList();
        data = data.Where(x => authService.HasAccess(CompanyConstants.AnyCompanyId, NextStateMapper(x.State)).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Terminals.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveAttributes(IAttributeService attributeService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = attributeService.Get().Where(x => x.State is State.Approve or State.Delete).ToList();
        data = data.Where(x => authService.HasAccess(CompanyConstants.AnyCompanyId, NextStateMapper(x.State)).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Attributes.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveUnits(IUnitService unitService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = unitService.Get().Where(x => x.State is State.Approve or State.Delete).ToList();
        data = data.Where(x => authService.HasAccess(CompanyConstants.AnyCompanyId, NextStateMapper(x.State)).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Units.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveQuantityDatums(IQuantityDatumService quantityDatumService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = quantityDatumService.Get().Where(x => x.State is State.Approve or State.Delete).ToList();
        data = data.Where(x => authService.HasAccess(CompanyConstants.AnyCompanyId, NextStateMapper(x.State)).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        QuantityDatums.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveRds(IRdsService rdsService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = rdsService.Get().Where(x => x.State is State.Approve or State.Delete).ToList();
        data = data.Where(x => authService.HasAccess(CompanyConstants.AnyCompanyId, NextStateMapper(x.State)).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Rds.AddRange(mappedData);
        return Task.CompletedTask;
    }

    private State NextStateMapper(State currentState)
    {
        return currentState switch
        {
            State.Approve => State.Approved,
            State.Delete => State.Deleted,
            _ => throw new MimirorgInvalidOperationException("It is not allowed to approve types that is not in approval state")
        };
    }
}