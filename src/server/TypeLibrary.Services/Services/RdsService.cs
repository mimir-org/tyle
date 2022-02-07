using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;
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
        public IEnumerable<RdsLibDm> GetRds(Aspect aspect)
        {
            var all = _rdsRepository.GetAll().Include(x => x.RdsCategory).ToList();
            return aspect == Aspect.NotSet ?
                all :
                all.Where(x => x.Aspect.HasFlag(aspect));
        }

       /// <summary>
       /// Get all RDS
       /// </summary>
       /// <returns></returns>
        public IEnumerable<RdsLibDm> GetRds()
        {
            var all = _rdsRepository.GetAll().Include(x => x.RdsCategory).ToList();
            return all;
        }

        /// <summary>
        /// Create a RDS
        /// </summary>
        /// <param name="rdsAm"></param>
        /// <returns></returns>
        public async Task<RdsLibDm> CreateRds(RdsLibAm rdsAm)
        {
            var data = await CreateRdsAsync(new List<RdsLibAm> { rdsAm });
            return data.SingleOrDefault();
        }

        /// <summary>
        /// Create RDS from a list
        /// </summary>
        /// <param name="createRds"></param>
        /// <returns></returns>
        public async Task<List<RdsLibDm>> CreateRdsAsync(List<RdsLibAm> createRds)
        {
            if (createRds == null || !createRds.Any())
                return new List<RdsLibDm>();

            var data = _mapper.Map<List<RdsLibDm>>(createRds);

            var existing = _rdsRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return new List<RdsLibDm>();

            foreach (var entity in notExisting)
            {
                await _rdsRepository.CreateAsync(entity);
            }
            await _rdsRepository.SaveAsync();
            return data;
        }

        public Task<IEnumerable<RdsCategoryLibAm>> GetRdsCategories()
        {
            var dataList = _rdsCategoryRepository.GetAll();
            var dataAm = _mapper.Map<List<RdsCategoryLibAm>>(dataList);
            return Task.FromResult<IEnumerable<RdsCategoryLibAm>>(dataAm);
        }

        public async Task<RdsCategoryLibAm> UpdateRdsCategory(RdsCategoryLibAm dataAm)
        {
            var data = _mapper.Map<RdsCategoryLibDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _rdsCategoryRepository.Update(data);
            await _rdsCategoryRepository.SaveAsync();
            return _mapper.Map<RdsCategoryLibAm>(data);
        }

        public async Task<RdsCategoryLibAm> CreateRdsCategory(RdsCategoryLibAm dataAm)
        {
            var data = _mapper.Map<RdsCategoryLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _rdsCategoryRepository.CreateAsync(data);
            await _rdsCategoryRepository.SaveAsync();
            return _mapper.Map<RdsCategoryLibAm>(createdData.Entity);
        }

        public async Task CreateRdsCategories(List<RdsCategoryLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<RdsCategoryLibDm>>(dataAm);
            var existing = _rdsCategoryRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _rdsCategoryRepository.Attach(data, EntityState.Added);
            }

            await _rdsCategoryRepository.SaveAsync();

            foreach (var data in notExisting)
                _rdsCategoryRepository.Detach(data);
        }
    }
}
