using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IBlobService
    {
        Task<BlobLibCm> CreateBlob(BlobLibAm blob, bool saveData = true);
        Task<IEnumerable<BlobLibCm>> CreateBlob(IEnumerable<BlobLibAm> blobDataList);
        Task<BlobLibCm> UpdateBlob(string id, BlobLibAm blob);
        IEnumerable<BlobLibCm> GetBlob();
    }
}
