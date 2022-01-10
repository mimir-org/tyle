using System.Threading.Tasks;

namespace TypeLibrary.Models.Abstract
{
    public interface IModelBuilderSyncService : IModuleInterface
    {
        Task SendData<T>(T data) where T : class;
        Task ReceiveData();
    }
}
