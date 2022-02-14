using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Contracts
{
    public interface IBlobService
    {
        Task<BlobLibDm> CreateBlob(BlobLibAm blob, bool saveData = true);
        Task<IEnumerable<BlobLibDm>> CreateBlob(IEnumerable<BlobLibAm> blobDataList);
        Task<BlobLibDm> UpdateBlob(string id, BlobLibAm blob);
        IEnumerable<BlobLibAm> GetBlob();
    }
}
