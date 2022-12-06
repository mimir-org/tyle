using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Records
{
    public record ApprovalData
    {
        private List<ApprovalCm> Nodes { get; } = new();

        public ICollection<ApprovalCm> GetAllData()
        {
            return Nodes;
        }

        public Task ResolveNodes(INodeService nodeService, IMapper mapper)
        {
            var data = nodeService.GetLatestVersions().Where(x => x.State is State.ApproveCompany or State.ApproveGlobal or State.Delete).ToList();
            var mappedData = mapper.Map<ICollection<ApprovalCm>>(data);
            Nodes.AddRange(mappedData);
            return Task.CompletedTask;
        }
    }
}
