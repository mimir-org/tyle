using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class RdsService : IRdsService
    {
        private readonly IMapper _mapper;
        private readonly IRdsRepository _rdsRepository;

        public RdsService(IMapper mapper, IRdsRepository rdsRepository)
        {
            _mapper = mapper;
            _rdsRepository = rdsRepository;
        }

        public async Task<ICollection<RdsLibCm>> Get()
        {
            var allRds = await _rdsRepository.Get();
            return _mapper.Map<List<RdsLibCm>>(allRds.ToList());
        }
    }
}