using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class QualifierService : IQualifierService
    {
        private readonly IMapper _mapper;
        private readonly IQualifierRepository _qualifierRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public QualifierService(IMapper mapper, IQualifierRepository qualifierRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _qualifierRepository = qualifierRepository;
            _contextAccessor = contextAccessor;
        }
        public Task<IEnumerable<QualifierAm>> GetQualifiers()
        {
            var dataList = _qualifierRepository.GetAll();
            var dataAm = _mapper.Map<List<QualifierAm>>(dataList);
            return Task.FromResult<IEnumerable<QualifierAm>>(dataAm);
        }

        public async Task<QualifierAm> UpdateQualifier(QualifierAm dataAm)
        {
            var data = _mapper.Map<QualifierDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _qualifierRepository.Update(data);
            await _qualifierRepository.SaveAsync();
            return _mapper.Map<QualifierAm>(data);
        }

        public async Task<QualifierAm> CreateQualifier(QualifierAm dataAm)
        {
            var data = _mapper.Map<QualifierDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _qualifierRepository.CreateAsync(data);
            await _qualifierRepository.SaveAsync();
            return _mapper.Map<QualifierAm>(createdData.Entity);
        }

        public async Task CreateQualifiers(List<QualifierAm> dataAm)
        {
            var dataList = _mapper.Map<List<QualifierDm>>(dataAm);
            var existing = _qualifierRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _qualifierRepository.Attach(data, EntityState.Added);
            }

            await _qualifierRepository.SaveAsync();

            foreach (var data in notExisting)
                _qualifierRepository.Detach(data);
        }
    }
}