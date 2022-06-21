using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TypeLibrary.Services.Contracts
{
    public interface IFileService
    {
        Task LoadDataFromFile(IFormFile file, CancellationToken cancellationToken);
        byte[] CreateFile();
    }
}