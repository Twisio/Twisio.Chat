using Portfol.io.Chat.Application.ViewModels;

namespace Portfol.io.Chat.Application.Repository
{
    public interface IChatRepository
    {
        Task<ChatsViewModel> GetChats(Guid userId);
        Task<ChatDto> GetChat(Guid chatId, Guid userId);
        Task<Guid> AddMessage(Guid chatId, string message, Guid userId);
        Task<Guid> CreateChat(Guid userId, Guid companionId);
    }
}
