using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Portfol.io.Chat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message, Guid companion, Guid chatId) 
        {
            await Clients.Users(companion.ToString(), Context.UserIdentifier!).SendAsync("Receive", message);
        }
    }
}
