using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class AttributeSourceService : IAttributeSourceService
    {
        private readonly IMapper _mapper;
        private readonly IEfAttributeSourceRepository _sourceRepository;
        private readonly ApplicationSettings _applicationSettings;

        public AttributeSourceService(IMapper mapper, IEfAttributeSourceRepository sourceRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _sourceRepository = sourceRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public Task<IEnumerable<AttributeSourceLibCm>> GetAttributeSources()
        {
            var notSet = _sourceRepository.FindBy(x => !x.Deleted && x.Name == Aspect.NotSet.ToString())?.First();
            var dataSet = _sourceRepository.GetAll().Where(x => !x.Deleted && x.Name != Aspect.NotSet.ToString()).ToList();

            var dataDmList = new List<AttributeSourceLibDm> { notSet };
            dataDmList.AddRange(dataSet.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            var dataCm = _mapper.Map<List<AttributeSourceLibCm>>(dataDmList);
            return Task.FromResult(dataCm.AsEnumerable());
        }

        public async Task<AttributeSourceLibCm> UpdateAttributeSource(AttributeSourceLibAm dataAm, int id)
        {
            if (dataAm == null)
                throw new MimirorgBadRequestException("The data object can not be null.");

            var existingDm = await _sourceRepository.GetAsync(id);

            if (existingDm?.Id == null)
                throw new MimirorgBadRequestException("Object not found.");

            if (existingDm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The object with id {id} is created by the system and can not be updated.");

            //TODO: The code below must be rewritten. What do we allow to be updated?
            var data = _mapper.Map<AttributeSourceLibDm>(dataAm);
            data.Id = id;
            _sourceRepository.Update(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<AttributeSourceLibCm>(data);
        }

        public async Task<AttributeSourceLibCm> CreateAttributeSource(AttributeSourceLibAm dataAm)
        {
            var data = _mapper.Map<AttributeSourceLibDm>(dataAm);
            var createdData = await _sourceRepository.CreateAsync(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<AttributeSourceLibCm>(createdData.Entity);
        }

        public async Task CreateAttributeSources(List<AttributeSourceLibAm> dataAm, bool createdBySystem = false)
        {
            var dataList = _mapper.Map<List<AttributeSourceLibDm>>(dataAm);
            var existing = _sourceRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Name != x.Name)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.CreatedBy = createdBySystem ? _applicationSettings.System : data.CreatedBy;
                _sourceRepository.Attach(data, EntityState.Added);
            }

            await _sourceRepository.SaveAsync();

            foreach (var data in notExisting)
                _sourceRepository.Detach(data);
        }
    }
}