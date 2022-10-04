using Portfol.io.Chat.Data;
using Portfol.io.Chat.Models;

namespace Portfol.io.Chat.Infrastructure.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly IChatDbContext _dbContext;

        public ChatRepository(IChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddMessage(Guid chatId, string text, Guid userId)
        {
            var message = new Message
            {
                Text = text,
                ChatId = chatId,
                Date = DateTime.Now,
                UserId = userId
            };

            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task CreateChat(Guid userId)
        {
            var chat = new AppChat
            {
                UserIds = new List<Guid> { userId }
            };

            await _dbContext.Chats.AddAsync(chat);
        }

        public AppChat GetChat(Guid companionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppChat> GetChats(Guid companionId)
        {
            throw new NotImplementedException();
        }

        public Task JoinChat(Guid companionId, Guid chatId)
        {
            throw new NotImplementedException();
        }
    }
}
