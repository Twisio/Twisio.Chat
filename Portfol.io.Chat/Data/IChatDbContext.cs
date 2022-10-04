using Microsoft.EntityFrameworkCore;
using Portfol.io.Chat.Models;

namespace Portfol.io.Chat.Data
{
    public interface IChatDbContext
    {
        DbSet<AppChat> Chats { get; set; }
        DbSet<Message> Messages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
