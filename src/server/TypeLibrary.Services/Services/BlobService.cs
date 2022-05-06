using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class BlobService : IBlobService
    {
        private readonly IMapper _mapper;
        private readonly IBlobRepository _blobDataRepository;

        public BlobService(IMapper mapper, IBlobRepository blobDataRepository)
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
        public async Task<BlobLibCm> CreateBlob(BlobLibAm blob, bool saveData = true)
        {
            var blobExist = await _blobDataRepository.GetAsync(blob.Id);

            if (blobExist != null)
                throw new MimirorgDuplicateException($"There is already an blob with name: {blob.Name} and discipline: {blob.Discipline}");

            var dm = _mapper.Map<BlobLibDm>(blob);
            await _blobDataRepository.CreateAsync(dm);

            if (saveData)
                await _blobDataRepository.SaveAsync();

            return _mapper.Map<BlobLibCm>(dm);
        }

        /// <summary>
        /// Create blob data from list
        /// </summary>
        /// <param name="blobDataList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BlobLibCm>> CreateBlob(IEnumerable<BlobLibAm> blobDataList)
        {
            var blobs = new List<BlobLibDm>();
            var existingBlobs = _blobDataRepository.GetAll().ToList();

            foreach (var blobData in blobDataList)
            {
                var blobExist = existingBlobs.FirstOrDefault(x => x.Id == blobData.Id);
                if (blobExist != null)
                    continue;

                var dm = _mapper.Map<BlobLibDm>(blobData);
                await _blobDataRepository.CreateAsync(dm);
                blobs.Add(dm);
            }

            await _blobDataRepository.SaveAsync();
            return _mapper.Map<List<BlobLibCm>>(blobs);
        }

        /// <summary>
        /// Update a blob
        /// </summary>
        /// <param name="id"></param>
        /// <param name="blob"></param>
        /// <returns></returns>
        public async Task<BlobLibCm> UpdateBlob(string id, BlobLibAm blob)
        {
            var dm = await _blobDataRepository.GetAsync(id);
            if (dm == null)
                throw new MimirorgNotFoundException($"There is no blob data with id: {id}");

            _blobDataRepository.Update(dm);
            await _blobDataRepository.SaveAsync();
            return _mapper.Map<BlobLibCm>(dm);
        }

        /// <summary>
        /// Get all blobs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlobLibCm> GetBlob()
        {
            var blobDms = _blobDataRepository.GetAll().ToList().OrderBy(x => x.Discipline).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            blobDms.Insert(0, new BlobLibDm
            {
                Discipline = Discipline.None,
                Data = null,
                Name = "No symbol"
            });

            return _mapper.Map<List<BlobLibCm>>(blobDms);
        }
    }
}
