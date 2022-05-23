using System.Threading.Tasks;

namespace TypeLibrary.Services.Contracts
{
    public interface IVersionService
    {
        Task<T> GetLatestVersion<T>(T obj) where T : class;
    }
}