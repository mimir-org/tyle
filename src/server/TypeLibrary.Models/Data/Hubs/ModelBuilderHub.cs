using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TypeLibrary.Models.Data.Hubs
{
    public class ModelBuilderHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
