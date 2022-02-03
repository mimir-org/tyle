using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using TypeLibrary.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class BlobDataService : IBlobDataService
    {
        private readonly IMapper _mapper;
        private readonly IBlobDataRepository _blobDataRepository;

        public BlobDataService(IMapper mapper, IBlobDataRepository blobDataRepository)
        {
            _mapper = mapper;
            _blobDataRepository = blobDataRepository;
        }

        /// <summary>
        /// Create blob data
        /// </summary>
        /// <param name="blobData"></param>
        /// <param name="saveData"></param>
        /// <returns></returns>
        public async Task<BlobDm> CreateBlobData(BlobDataAm blobData, bool saveData = true)
        {
            if (!string.IsNullOrEmpty(blobData.Id))
                return await UpdateBlobData(blobData);

            var dm = _mapper.Map<BlobDm>(blobData);
            dm.Id = dm.Key.CreateMd5();

            var blobExist = await _blobDataRepository.GetAsync(dm.Id);

            if (blobExist != null)
            {
                blobData.Id = dm.Id;
                _mapper.Map(blobData, blobExist);
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
        public async Task<IEnumerable<BlobDm>> CreateBlobData(IEnumerable<BlobDataAm> blobDataList)
        {
            var blobs = new List<BlobDm>();

            foreach (var blobData in blobDataList)
            {
                blobs.Add(await CreateBlobData(blobData, false));
            }

            await _blobDataRepository.SaveAsync();
            return blobs;
        }

        /// <summary>
        /// Update a blob
        /// </summary>
        /// <param name="blobData"></param>
        /// <returns></returns>
        public async Task<BlobDm> UpdateBlobData(BlobDataAm blobData)
        {
            var dm = await _blobDataRepository.GetAsync(blobData.Id);
            if (dm == null)
                throw new MimirorgNotFoundException($"There is no blob data with id: {blobData.Id}");

            _blobDataRepository.Update(dm);
            await _blobDataRepository.SaveAsync();
            return dm;
        }

        /// <summary>
        /// Get all blobs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlobDataAm> GetBlobData()
        {
            var dms = _blobDataRepository.GetAll()
                .OrderBy(x => x.Name)
                .ProjectTo<BlobDataAm>(_mapper.ConfigurationProvider)
                .ToList();

            dms.Insert(0, new BlobDataAm
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
