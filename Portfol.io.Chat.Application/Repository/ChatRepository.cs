using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Portfol.io.Chat.Application.Exceptions;
using Portfol.io.Chat.Application.Interfaces;
using Portfol.io.Chat.Application.ViewModels;
using Portfol.io.Chat.Domain;

namespace Portfol.io.Chat.Application.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly IChatDbContext _dbContext;
        private readonly IMapper _mapper;

        public ChatRepository(IChatDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> AddMessage(Guid chatId, string text, Guid userId)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                Text = text,
                ChatId = chatId,
                Date = DateTime.UtcNow,
                UserId = userId
            };

            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return message.Id;
        }

        public async Task<Guid> CreateChat(Guid userId, Guid companionId)
        {
            var chat = new AppChat
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CompanionId = companionId
            };

            await _dbContext.Chats.AddAsync(chat);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return chat.Id;
        }

        public async Task<ChatDto> GetChat(Guid chatId, Guid userId)
        {
            var chat = await _dbContext.Chats.Include(u => u.Messages).FirstOrDefaultAsync(u => (u.UserId == userId || u.CompanionId == userId) && u.Id == chatId);

            if (chat is null) throw new NotFoundException(nameof(AppChat), new { userId, chatId });

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<ChatsViewModel> GetChats(Guid userId)
        {
            var chats = await _dbContext.Chats.Where(u => (u.UserId == userId) || (u.CompanionId == userId)).ProjectTo<ChatLookupDto>(_mapper.ConfigurationProvider).ToListAsync();

            if (chats.Count() == 0) throw new NotFoundException(nameof(AppChat), userId);

            return new ChatsViewModel { Chats = chats };
        }
    }
}
