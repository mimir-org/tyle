using System.Collections.Generic;
using System.Threading.Tasks;

using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface IBlobDataService
    {
        Task<BlobDm> CreateBlobData(BlobDataAm blobData, bool saveData = true);
        Task<IEnumerable<BlobDm>> CreateBlobData(IEnumerable<BlobDataAm> blobDataList);
        Task<BlobDm> UpdateBlobData(BlobDataAm blobData);
        IEnumerable<BlobDataAm> GetBlobData();
    }
}
