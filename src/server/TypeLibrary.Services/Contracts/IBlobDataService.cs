using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Services.Contracts
{
    public interface IBlobDataService
    {
        Task<BlobLibDm> CreateBlobData(BlobDataLibAm blobData, bool saveData = true);
        Task<IEnumerable<BlobLibDm>> CreateBlobData(IEnumerable<BlobDataLibAm> blobDataList);
        Task<BlobLibDm> UpdateBlobData(BlobDataLibAm blobData);
        IEnumerable<BlobDataLibAm> GetBlobData();
    }
}
