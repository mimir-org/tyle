using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Records
{
    public record ApprovalData
    {
        private List<ApprovalCm> Nodes { get; } = new();
        private List<ApprovalCm> Transports { get; } = new();
        private List<ApprovalCm> Interfaces { get; } = new();
        private List<ApprovalCm> Terminals { get; } = new();

        public ICollection<ApprovalCm> GetAllData()
        {
            return Nodes.Union(Transports).Union(Interfaces).Union(Terminals).ToList();
        }

        public Task ResolveNodes(INodeService nodeService, IMapper mapper, IMimirorgAuthService authService)
        {
            var data = nodeService.GetLatestVersions().Where(x => x.State is State.ApproveCompany or State.ApproveGlobal or State.Delete).ToList();
            data = data.Where(x => authService.HasAccess(x.CompanyId, NextStateMapper(x.State)).Result).ToList();
            var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
            Nodes.AddRange(mappedData);
            return Task.CompletedTask;
        }

        public Task ResolveTransports(ITransportService transportService, IMapper mapper, IMimirorgAuthService authService)
        {
            var data = transportService.GetLatestVersions().Where(x => x.State is State.ApproveCompany or State.ApproveGlobal or State.Delete).ToList();
            data = data.Where(x => authService.HasAccess(x.CompanyId, NextStateMapper(x.State)).Result).ToList();
            var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
            Transports.AddRange(mappedData);
            return Task.CompletedTask;
        }

        public Task ResolveInterfaces(IInterfaceService interfaceService, IMapper mapper, IMimirorgAuthService authService)
        {
            var data = interfaceService.GetLatestVersions().Where(x => x.State is State.ApproveCompany or State.ApproveGlobal or State.Delete).ToList();
            data = data.Where(x => authService.HasAccess(x.CompanyId, NextStateMapper(x.State)).Result).ToList();
            var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
            Interfaces.AddRange(mappedData);
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
}