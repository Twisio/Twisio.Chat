using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Portfol.io.Chat.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub { }
}