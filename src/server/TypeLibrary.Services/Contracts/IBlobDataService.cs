using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface IBlobDataService
    {
        Task<BlobData> CreateBlobData(BlobDataAm blobData, bool saveData = true);
        Task<IEnumerable<BlobData>> CreateBlobData(IEnumerable<BlobDataAm> blobDataList);
        Task<BlobData> UpdateBlobData(BlobDataAm blobData);
        IEnumerable<BlobDataAm> GetBlobData();
    }
}
