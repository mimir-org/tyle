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
    public class AttributeFormatService : IAttributeFormatService
    {
        private readonly IMapper _mapper;
        private readonly IEfAttributeFormatDbRepository _formatRepository;
        private readonly ApplicationSettings _applicationSettings;

        public AttributeFormatService(IMapper mapper, IEfAttributeFormatDbRepository formatRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _formatRepository = formatRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public Task<IEnumerable<AttributeFormatLibCm>> GetAttributeFormats()
        {
            var notSet = _formatRepository.FindBy(x => !x.Deleted && x.Name == Aspect.NotSet.ToString())?.First();
            var dataSet = _formatRepository.GetAll().Where(x => !x.Deleted && x.Name != Aspect.NotSet.ToString()).ToList();

            var dataDmList = new List<AttributeFormatLibDm> { notSet };
            dataDmList.AddRange(dataSet.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            var dataCm = _mapper.Map<List<AttributeFormatLibCm>>(dataDmList);
            return Task.FromResult(dataCm.AsEnumerable());
        }

        public async Task<AttributeFormatLibCm> UpdateAttributeFormat(AttributeFormatLibAm dataAm, int id)
        {
            if (dataAm == null)
                throw new MimirorgBadRequestException("The data object can not be null.");

            var existingDm = await _formatRepository.GetAsync(id);

            if (existingDm?.Id == null)
                throw new MimirorgBadRequestException("Object not found.");

            if (existingDm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The object with id {id} is created by the system and can not be updated.");

            //TODO: The code below must be rewritten. What do we allow to be updated?
            var data = _mapper.Map<AttributeFormatLibDm>(dataAm);
            data.Id = id;
            _formatRepository.Update(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<AttributeFormatLibCm>(data);
        }

        public async Task<AttributeFormatLibCm> CreateAttributeFormat(AttributeFormatLibAm dataAm)
        {
            var data = _mapper.Map<AttributeFormatLibDm>(dataAm);
            var createdData = await _formatRepository.CreateAsync(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<AttributeFormatLibCm>(createdData.Entity);
        }

        public async Task CreateAttributeFormats(List<AttributeFormatLibAm> dataAm, bool createdBySystem = false)
        {
            var dataList = _mapper.Map<List<AttributeFormatLibDm>>(dataAm);
            var existing = _formatRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Name != x.Name)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.CreatedBy = createdBySystem ? _applicationSettings.System : data.CreatedBy;
                _formatRepository.Attach(data, EntityState.Added);
            }

            await _formatRepository.SaveAsync();

            foreach (var data in notExisting)
                _formatRepository.Detach(data);
        }
    }
}