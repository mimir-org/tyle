using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Records;

public record ApprovalData
{
    private List<ApprovalCm> AspectObjects { get; } = new();
    private List<ApprovalCm> Terminals { get; } = new();

    public ICollection<ApprovalCm> GetAllData()
    {
        return AspectObjects.Union(Terminals).ToList();
    }

    public Task ResolveAspectObjects(IAspectObjectService aspectObjectService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = aspectObjectService.GetLatestVersions().Where(x => x.State is State.ApproveCompany or State.ApproveGlobal or State.Delete).ToList();
        data = data.Where(x => authService.HasAccess(x.CompanyId, NextStateMapper(x.State)).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        AspectObjects.AddRange(mappedData);
        return Task.CompletedTask;
    }

    public Task ResolveTerminals(ITerminalService terminalService, IMapper mapper, IMimirorgAuthService authService)
    {
        var data = terminalService.GetLatestVersions().Where(x => x.State is State.ApproveCompany or State.ApproveGlobal or State.Delete).ToList();
        data = data.Where(x => authService.HasAccess(x.CompanyId, NextStateMapper(x.State)).Result).ToList();
        var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
        Terminals.AddRange(mappedData);
        return Task.CompletedTask;
    }

    private State NextStateMapper(State currentState)
    {
        return currentState switch
        {
            State.ApproveCompany => State.ApprovedCompany,
            State.Delete => State.Deleted,
            State.ApproveGlobal => State.ApprovedGlobal,
            _ => throw new MimirorgInvalidOperationException("It is not allowed to approve types that is not in approval state")
        };
    }
}