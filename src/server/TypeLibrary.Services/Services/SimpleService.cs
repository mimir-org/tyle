using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class SimpleService : ISimpleService
    {
        private readonly IMapper _mapper;
        private readonly ISimpleRepository _simpleRepository;
        private readonly ApplicationSettings _applicationSettings;

        public SimpleService(IMapper mapper, IOptions<ApplicationSettings> applicationSettings, ISimpleRepository simpleRepository)
        {
            _mapper = mapper;
            _simpleRepository = simpleRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public async Task<SimpleLibCm> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get simple. The id is missing value.");

            var data = await _simpleRepository.Get(id);

            if (data == null)
                throw new MimirorgNotFoundException($"There is no simple with id: {id}");

            var simpleLibCm = _mapper.Map<SimpleLibCm>(data);

            if (simpleLibCm == null)
                throw new MimirorgMappingException("SimpleLibDm", "SimpleLibCm");

            return simpleLibCm;
        }

        public Task<IEnumerable<SimpleLibCm>> Get()
        {
            var simpleLibDms = _simpleRepository.Get().Where(x => x.State != State.Deleted).OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var simpleLibCms = _mapper.Map<IEnumerable<SimpleLibCm>>(simpleLibDms);

            if (simpleLibDms.Any() && (simpleLibCms == null || !simpleLibCms.Any()))
                throw new MimirorgMappingException("List<SimpleLibDm>", "ICollection<SimpleLibAm>");

            return Task.FromResult(simpleLibCms ?? new List<SimpleLibCm>());
        }

        public async Task<IEnumerable<SimpleLibCm>> Create(IEnumerable<SimpleLibAm> simpleAms, bool createdBySystem = false)
        {
            var validation = simpleAms.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Couldn't create simple", validation);

            var existing = _simpleRepository.Get().ToList();
            var simpleToCreate = simpleAms.Where(x => existing.All(y => y.Id != x.Id)).ToList();
            var simpleDmList = _mapper.Map<IEnumerable<SimpleLibDm>>(simpleToCreate).ToList();

            if (simpleDmList == null || !simpleDmList.Any() && simpleToCreate.Any())
                throw new MimirorgMappingException("ICollection<SimpleLibDm>", "ICollection<SimpleLibAm>");

            foreach (var simpleDm in simpleDmList)
            {
                simpleDm.CreatedBy = createdBySystem ? _applicationSettings.System : simpleDm.CreatedBy;
                await _simpleRepository.Create(simpleDm, createdBySystem ? State.ApprovedGlobal : State.Draft);
            }

            _simpleRepository.ClearAllChangeTrackers();

            return _mapper.Map<ICollection<SimpleLibCm>>(simpleDmList);
        }
    }
}