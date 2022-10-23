using Microsoft.EntityFrameworkCore;
using Portfol.io.Chat.Domain;

namespace Portfol.io.Chat.Application.Interfaces
{
    public interface IChatDbContext
    {
        DbSet<AppChat> Chats { get; set; }
        DbSet<Message> Messages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
