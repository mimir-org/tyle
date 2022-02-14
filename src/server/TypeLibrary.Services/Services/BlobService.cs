using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;
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
            var id = blob.Key.CreateMd5();
            var blobExist = await _blobDataRepository.GetAsync(id);

            if(blobExist != null)
                throw new MimirorgDuplicateException($"There is already an blob with name: {blob.Name} and discipline: {blob.Discipline}");

            var dm = _mapper.Map<BlobLibDm>(blob);
            dm.Id = id;
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
        /// <param name="id"></param>
        /// <param name="blob"></param>
        /// <returns></returns>
        public async Task<BlobLibDm> UpdateBlob(string id, BlobLibAm blob)
        {
            var dm = await _blobDataRepository.GetAsync(id);
            if (dm == null)
                throw new MimirorgNotFoundException($"There is no blob data with id: {id}");

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
                Name = "No symbol"
            });

            return dms;
        }
    }
}
