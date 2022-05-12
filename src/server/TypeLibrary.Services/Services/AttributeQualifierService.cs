using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class AttributeQualifierService : IAttributeQualifierService
    {
        private readonly IMapper _mapper;
        private readonly IEfAttributeQualifierRepository _qualifierRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationSettings _applicationSettings;

        public AttributeQualifierService(IMapper mapper, IEfAttributeQualifierRepository qualifierRepository, IHttpContextAccessor contextAccessor, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _qualifierRepository = qualifierRepository;
            _contextAccessor = contextAccessor;
            _applicationSettings = applicationSettings?.Value;
        }
        public Task<IEnumerable<AttributeQualifierLibCm>> GetAttributeQualifiers()
        {
            var notSet = _qualifierRepository.FindBy(x => !x.Deleted && x.Name == Aspect.NotSet.ToString())?.First();
            var dataSet = _qualifierRepository.GetAll().Where(x => !x.Deleted && x.Name != Aspect.NotSet.ToString()).ToList();

            var dataDmList = new List<AttributeQualifierLibDm> { notSet };
            dataDmList.AddRange(dataSet.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            var dataCm = _mapper.Map<List<AttributeQualifierLibCm>>(dataDmList);
            return Task.FromResult(dataCm.AsEnumerable());
        }

        public async Task<AttributeQualifierLibCm> UpdateAttributeQualifier(AttributeQualifierLibAm dataAm, int id)
        {
            if (dataAm == null)
                throw new MimirorgBadRequestException("The data object can not be null.");

            var existingDm = await _qualifierRepository.GetAsync(id);

            if (existingDm?.Id == null)
                throw new MimirorgBadRequestException("Object not found.");

            if (existingDm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The object with id {id} is created by the system and can not be updated.");

            //TODO: The code below must be rewritten. What do we allow to be updated?
            var data = _mapper.Map<AttributeQualifierLibDm>(dataAm);
            data.Id = id;
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _qualifierRepository.Update(data);
            await _qualifierRepository.SaveAsync();
            return _mapper.Map<AttributeQualifierLibCm>(data);
        }

        public async Task<AttributeQualifierLibCm> CreateAttributeQualifier(AttributeQualifierLibAm dataAm)
        {
            var data = _mapper.Map<AttributeQualifierLibDm>(dataAm);
            var createdData = await _qualifierRepository.CreateAsync(data);
            await _qualifierRepository.SaveAsync();
            return _mapper.Map<AttributeQualifierLibCm>(createdData.Entity);
        }

        public async Task CreateAttributeQualifiers(List<AttributeQualifierLibAm> dataAm, bool createdBySystem = false)
        {
            var dataList = _mapper.Map<List<AttributeQualifierLibDm>>(dataAm);
            var existing = _qualifierRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Name != x.Name)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.CreatedBy = createdBySystem ? _applicationSettings.System : data.CreatedBy;
                _qualifierRepository.Attach(data, EntityState.Added);
            }

            await _qualifierRepository.SaveAsync();

            foreach (var data in notExisting)
                _qualifierRepository.Detach(data);
        }
    }
}