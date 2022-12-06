using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;
using TypeLibrary.Services.Records;

namespace TypeLibrary.Services.Services
{
    public class ApprovalService : IApprovalService
    {
        private readonly IMapper _mapper;
        private readonly INodeService _nodeService;

        public ApprovalService(IMapper mapper, INodeService nodeService)
        {
            _mapper = mapper;
            _nodeService = nodeService;
        }

        public async Task<ICollection<ApprovalCm>> GetApprovals()
        {
            var data = new ApprovalData();

            var tasks = new List<Task>
            {
                Task.Run(() => data.ResolveNodes(_nodeService, _mapper))
            };
            
            await Task.WhenAll(tasks);
            return data.GetAllData();
        }
    }
}
