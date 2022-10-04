using Microsoft.EntityFrameworkCore;
using Portfol.io.Chat.Models;

namespace Portfol.io.Chat.Data
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
