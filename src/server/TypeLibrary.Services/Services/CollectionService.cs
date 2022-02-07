using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly IMapper _mapper;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public CollectionService(IMapper mapper, ICollectionRepository collectionRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _collectionRepository = collectionRepository;
            _contextAccessor = contextAccessor;
        }

        public Task<IEnumerable<CollectionLibAm>> GetCollections()
        {
            var dataList = _collectionRepository.GetAll();
            var dataAm = _mapper.Map<List<CollectionLibAm>>(dataList);
            return Task.FromResult<IEnumerable<CollectionLibAm>>(dataAm);
        }

        public async Task<CollectionLibAm> UpdateCollection(CollectionLibAm dataAm)
        {
            var data = _mapper.Map<CollectionLibDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _collectionRepository.Update(data);
            await _collectionRepository.SaveAsync();
            return _mapper.Map<CollectionLibAm>(data);
        }

        public async Task<CollectionLibAm> CreateCollection(CollectionLibAm dataAm)
        {
            var data = _mapper.Map<CollectionLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _collectionRepository.CreateAsync(data);
            await _collectionRepository.SaveAsync();
            return _mapper.Map<CollectionLibAm>(createdData.Entity);
        }

        public async Task CreateCollections(List<CollectionLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<CollectionLibDm>>(dataAm);
            var existing = _collectionRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _collectionRepository.Attach(data, EntityState.Added);
            }

            await _collectionRepository.SaveAsync();

            foreach (var data in notExisting)
                _collectionRepository.Detach(data);
        }
    }
}