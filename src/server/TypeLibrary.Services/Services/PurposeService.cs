using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class PurposeService : IPurposeService
    {
        private readonly IMapper _mapper;
        private readonly IPurposeRepository _purposeRepository;

        public PurposeService(IMapper mapper, IPurposeRepository purposeRepository)
        {
            _mapper = mapper;
            _purposeRepository = purposeRepository;
        }

        public async Task<ICollection<PurposeLibCm>> Get()
        {
            var dataList = await _purposeRepository.Get();
            return _mapper.Map<List<PurposeLibCm>>(dataList);
        }
    }
}