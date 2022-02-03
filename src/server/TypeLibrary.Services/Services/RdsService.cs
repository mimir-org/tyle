using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TypeLibrary.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;
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
        public IEnumerable<RdsDm> GetRds(Aspect aspect)
        {
            var all = _rdsRepository.GetAll().Include(x => x.RdsCategoryDm).ToList();
            return aspect == Aspect.NotSet ?
                all :
                all.Where(x => x.Aspect.HasFlag(aspect));
        }

       /// <summary>
       /// Get all RDS
       /// </summary>
       /// <returns></returns>
        public IEnumerable<RdsDm> GetRds()
        {
            var all = _rdsRepository.GetAll().Include(x => x.RdsCategoryDm).ToList();
            return all;
        }

        /// <summary>
        /// Create a RDS
        /// </summary>
        /// <param name="rdsAm"></param>
        /// <returns></returns>
        public async Task<RdsDm> CreateRds(RdsAm rdsAm)
        {
            var data = await CreateRdsAsync(new List<RdsAm> { rdsAm });
            return data.SingleOrDefault();
        }

        /// <summary>
        /// Create RDS from a list
        /// </summary>
        /// <param name="createRds"></param>
        /// <returns></returns>
        public async Task<List<RdsDm>> CreateRdsAsync(List<RdsAm> createRds)
        {
            if (createRds == null || !createRds.Any())
                return new List<RdsDm>();

            var data = _mapper.Map<List<RdsDm>>(createRds);

            var existing = _rdsRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return new List<RdsDm>();

            foreach (var entity in notExisting)
            {
                await _rdsRepository.CreateAsync(entity);
            }
            await _rdsRepository.SaveAsync();
            return data;
        }

        public Task<IEnumerable<RdsCategoryAm>> GetRdsCategories()
        {
            var dataList = _rdsCategoryRepository.GetAll();
            var dataAm = _mapper.Map<List<RdsCategoryAm>>(dataList);
            return Task.FromResult<IEnumerable<RdsCategoryAm>>(dataAm);
        }

        public async Task<RdsCategoryAm> UpdateRdsCategory(RdsCategoryAm dataAm)
        {
            var data = _mapper.Map<RdsCategoryDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _rdsCategoryRepository.Update(data);
            await _rdsCategoryRepository.SaveAsync();
            return _mapper.Map<RdsCategoryAm>(data);
        }

        public async Task<RdsCategoryAm> CreateRdsCategory(RdsCategoryAm dataAm)
        {
            var data = _mapper.Map<RdsCategoryDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _rdsCategoryRepository.CreateAsync(data);
            await _rdsCategoryRepository.SaveAsync();
            return _mapper.Map<RdsCategoryAm>(createdData.Entity);
        }

        public async Task CreateRdsCategories(List<RdsCategoryAm> dataAm)
        {
            var dataList = _mapper.Map<List<RdsCategoryDm>>(dataAm);
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
