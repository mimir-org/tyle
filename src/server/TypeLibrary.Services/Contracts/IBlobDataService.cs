using System.Collections.Generic;
using System.Threading.Tasks;
using Mb.Models.Application;
using Mb.Models.Data.TypeEditor;

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
