using Portfol.io.Chat.Models;

namespace Portfol.io.Chat.Infrastructure.Repository
{
    public interface IChatRepository
    {
        IEnumerable<AppChat> GetChats(Guid companionId);
        AppChat GetChat(Guid companionId);
        Task AddMessage(Guid chatId, string message, Guid userId);
        Task CreateChat(Guid userId);
        Task JoinChat(Guid companionId, Guid chatId);
    }
}
