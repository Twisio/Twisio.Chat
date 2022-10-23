using Microsoft.EntityFrameworkCore;
using Portfol.io.Chat.Application.Interfaces;
using Portfol.io.Chat.Domain;

namespace Portfol.io.Chat.Persistence
{
    public class ChatDbContext : DbContext, IChatDbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options) { }

        public DbSet<AppChat> Chats { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
