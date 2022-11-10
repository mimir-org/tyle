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
        private readonly IPurposeReferenceRepository _purposeReferenceRepository;

        public PurposeService(IMapper mapper, IPurposeReferenceRepository purposeReferenceRepository)
        {
            _mapper = mapper;
            _purposeReferenceRepository = purposeReferenceRepository;
        }

        /// <summary>
        /// Get all purposes
        /// </summary>
        /// <returns>List of purposes></returns>
        public async Task<ICollection<PurposeLibCm>> Get()
        {
            var dataSet = await _purposeReferenceRepository.Get();
            return _mapper.Map<List<PurposeLibCm>>(dataSet);
        }
    }
}