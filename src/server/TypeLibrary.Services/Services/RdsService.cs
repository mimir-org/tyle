using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
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
        private readonly IRdsCategoryRepository _rdsCategoryRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public RdsService(IMapper mapper, IRdsRepository rdsRepository, IRdsCategoryRepository rdsCategoryRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _rdsRepository = rdsRepository;
            _rdsCategoryRepository = rdsCategoryRepository;
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Get all RDS by aspect
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns></returns>
        public IEnumerable<RdsLibCm> GetRds(Aspect aspect)
        {
            var all = _rdsRepository.GetAll().Include(x => x.RdsCategory).ToList();
            var rds = aspect == Aspect.NotSet ?
                all :
                all.Where(x => x.Aspect.HasFlag(aspect));

            return _mapper.Map<List<RdsLibCm>>(rds);
        }

       /// <summary>
       /// Get all RDS
       /// </summary>
       /// <returns></returns>
        public IEnumerable<RdsLibCm> GetRds()
        {
            var all = _rdsRepository.GetAll().Include(x => x.RdsCategory).ToList();
            return _mapper.Map<List<RdsLibCm>>(all);
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

        public Task<IEnumerable<RdsCategoryLibCm>> GetRdsCategories()
        {
            var dataList = _rdsCategoryRepository.GetAll();
            var dataAm = _mapper.Map<List<RdsCategoryLibCm>>(dataList);
            return Task.FromResult(dataAm.AsEnumerable());
        }

        public async Task<RdsCategoryLibCm> UpdateRdsCategory(RdsCategoryLibAm dataAm)
        {
            var data = _mapper.Map<RdsCategoryLibDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _rdsCategoryRepository.Update(data);
            await _rdsCategoryRepository.SaveAsync();
            return _mapper.Map<RdsCategoryLibCm>(data);
        }

        public async Task<RdsCategoryLibCm> CreateRdsCategory(RdsCategoryLibAm dataAm)
        {
            var data = _mapper.Map<RdsCategoryLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            var createdData = await _rdsCategoryRepository.CreateAsync(data);
            await _rdsCategoryRepository.SaveAsync();
            return _mapper.Map<RdsCategoryLibCm>(createdData.Entity);
        }

        public async Task CreateRdsCategories(List<RdsCategoryLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<RdsCategoryLibDm>>(dataAm);
            var existing = _rdsCategoryRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                _rdsCategoryRepository.Attach(data, EntityState.Added);
            }

            await _rdsCategoryRepository.SaveAsync();

            foreach (var data in notExisting)
                _rdsCategoryRepository.Detach(data);
        }
    }
}
