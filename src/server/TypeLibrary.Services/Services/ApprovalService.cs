using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;
using TypeLibrary.Services.Records;

namespace TypeLibrary.Services.Services
{
    public class ApprovalService : IApprovalService
    {
        private readonly IMapper _mapper;
        private readonly INodeService _nodeService;
        private readonly ITerminalService _terminalService;
        private readonly IMimirorgAuthService _authService;

        public ApprovalService(IMapper mapper, INodeService nodeService, ITerminalService terminalService, IMimirorgAuthService authService)
        {
            _mapper = mapper;
            _nodeService = nodeService;
            _terminalService = terminalService;
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
                Task.Run(() => data.ResolveNodes(_nodeService, _mapper, _authService)),
                Task.Run(() => data.ResolveTerminals(_terminalService, _mapper, _authService))
            };

            await Task.WhenAll(tasks);
            return data.GetAllData();
        }
    }
}