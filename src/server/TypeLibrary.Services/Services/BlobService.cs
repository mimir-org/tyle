using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class BlobService : IBlobService
    {
        private readonly IMapper _mapper;
        private readonly IBlobDataRepository _blobDataRepository;

        public BlobService(IMapper mapper, IBlobDataRepository blobDataRepository)
        {
            _mapper = mapper;
            _blobDataRepository = blobDataRepository;
        }

        /// <summary>
        /// Create blob data
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="saveData"></param>
        /// <returns></returns>
        public async Task<BlobLibDm> CreateBlob(BlobLibAm blob, bool saveData = true)
        {
            if (!string.IsNullOrEmpty(blob.Id))
                return await UpdateBlob(blob);

            var dm = _mapper.Map<BlobLibDm>(blob);
            dm.Id = dm.Key.CreateMd5();

            var blobExist = await _blobDataRepository.GetAsync(dm.Id);

            if (blobExist != null)
            {
                blob.Id = dm.Id;
                _mapper.Map(blob, blobExist);
                _blobDataRepository.Attach(blobExist, EntityState.Modified);

                if (saveData)
                    await _blobDataRepository.SaveAsync();

                return blobExist;
            }

            await _blobDataRepository.CreateAsync(dm);

            if (saveData)
                await _blobDataRepository.SaveAsync();

            return dm;
        }

        /// <summary>
        /// Create blob data from list
        /// </summary>
        /// <param name="blobDataList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BlobLibDm>> CreateBlob(IEnumerable<BlobLibAm> blobDataList)
        {
            var blobs = new List<BlobLibDm>();

            foreach (var blobData in blobDataList)
            {
                blobs.Add(await CreateBlob(blobData, false));
            }

            await _blobDataRepository.SaveAsync();
            return blobs;
        }

        /// <summary>
        /// Update a blob
        /// </summary>
        /// <param name="blob"></param>
        /// <returns></returns>
        public async Task<BlobLibDm> UpdateBlob(BlobLibAm blob)
        {
            var dm = await _blobDataRepository.GetAsync(blob.Id);
            if (dm == null)
                throw new MimirorgNotFoundException($"There is no blob data with id: {blob.Id}");

            _blobDataRepository.Update(dm);
            await _blobDataRepository.SaveAsync();
            return dm;
        }

        /// <summary>
        /// Get all blobs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlobLibAm> GetBlob()
        {
            var dms = _blobDataRepository.GetAll()
                .OrderBy(x => x.Name)
                .ProjectTo<BlobLibAm>(_mapper.ConfigurationProvider)
                .ToList();

            dms.Insert(0, new BlobLibAm
            {
                Discipline = Discipline.None,
                Data = null,
                Id = null,
                Name = "No symbol"
            });

            return dms;
        }
    }
}
