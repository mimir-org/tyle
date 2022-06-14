using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.Authentication.Contracts
{
    public interface ITimedHookService
    {
        /// <summary>
        /// Hook queue
        /// Send message to all clients that have registered a key 
        /// </summary>
        Queue<CacheKey> HookQueue { get; set; }
    }
}
