using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfol.io.Chat.Application.Interfaces;

namespace Portfol.io.Chat.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection _services, string connectionString)
        {
            _services.AddDbContext<ChatDbContext>(option => option.UseNpgsql(connectionString));
            _services.AddScoped<IChatDbContext>(provider => provider.GetService<ChatDbContext>()!);

            return _services;
        }
    }
}
