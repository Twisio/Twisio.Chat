using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Portfol.io.Chat.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Enter(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
    }
}