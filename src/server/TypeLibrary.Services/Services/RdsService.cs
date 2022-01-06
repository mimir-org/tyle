using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mb.Models.Application.TypeEditor;
using Mb.Models.Data.TypeEditor;
using Mb.Models.Enums;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Get all RDS by aspect
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns></returns>
        public IEnumerable<Rds> GetRds(Aspect aspect)
        {
            var all = _rdsRepository.GetAll().Include(x => x.RdsCategory).ToList();
            return aspect == Aspect.NotSet ?
                all :
                all.Where(x => x.Aspect.HasFlag(aspect));
        }

        /// <summary>
        /// Create a RDS
        /// </summary>
        /// <param name="createRds"></param>
        /// <returns></returns>
        public async Task<Rds> CreateRds(CreateRds createRds)
        {
            var data = await CreateRdsAsync(new List<CreateRds> { createRds });
            return data.SingleOrDefault();
        }

        /// <summary>
        /// Create RDS from a list
        /// </summary>
        /// <param name="createRds"></param>
        /// <returns></returns>
        public async Task<List<Rds>> CreateRdsAsync(List<CreateRds> createRds)
        {
            if (createRds == null || !createRds.Any())
                return new List<Rds>();

            var data = _mapper.Map<List<Rds>>(createRds);

            var existing = _rdsRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return new List<Rds>();

            foreach (var entity in notExisting)
            {
                await _rdsRepository.CreateAsync(entity);
            }
            await _rdsRepository.SaveAsync();
            return data;
        }
    }
}
