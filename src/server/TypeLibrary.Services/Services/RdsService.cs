using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
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

        /// <summary>
        /// Get all RDS
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RdsLibCm> GetRds()
        {
            var allRds = _rdsRepository.GetAll()
                .OrderBy(x => x.Id.Length)
                .ThenBy(x => x.Id, StringComparer.InvariantCultureIgnoreCase)
                .ToList();

            return _mapper.Map<List<RdsLibCm>>(allRds);
        }

        /// <summary>
        /// Create a RDS
        /// </summary>
        /// <param name="rdsAm"></param>
        /// <returns></returns>
        public async Task<RdsLibCm> CreateRds(RdsLibAm rdsAm)
        {
            var data = await CreateRdsAsync(new List<RdsLibAm> { rdsAm });
            return data.SingleOrDefault();
        }

        /// <summary>
        /// Create RDS from a list
        /// </summary>
        /// <param name="createRds"></param>
        /// <returns></returns>
        public async Task<List<RdsLibCm>> CreateRdsAsync(List<RdsLibAm> createRds)
        {
            if (createRds == null || !createRds.Any())
                return new List<RdsLibCm>();

            var data = _mapper.Map<List<RdsLibDm>>(createRds);

            var existing = _rdsRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return new List<RdsLibCm>();

            foreach (var entity in notExisting)
            {
                await _rdsRepository.CreateAsync(entity);
            }
            await _rdsRepository.SaveAsync();
            return _mapper.Map<List<RdsLibCm>>(data);
        }
    }
}
