using System.Threading.Tasks;

namespace TypeLibrary.Services.Contracts
{
    public interface ISeedingService
    {
        Task LoadDataFromFiles();
    }
}