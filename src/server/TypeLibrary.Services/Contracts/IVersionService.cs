using System.Threading.Tasks;

namespace TypeLibrary.Services.Contracts
{
    public interface IVersionService
    {
        Task<T> GetLatestVersion<T>(T obj) where T : class;
        Task<string> CalculateNewVersion<T, TY>(T latestVersionDm, TY newAm) where T : class where TY : class;
    }
}