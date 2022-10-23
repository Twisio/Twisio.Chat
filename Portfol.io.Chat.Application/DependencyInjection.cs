using Microsoft.Extensions.DependencyInjection;
using Portfol.io.Chat.Application.Common.Mappings;
using Portfol.io.Chat.Application.Repository;
using System.Reflection;

namespace Portfol.io.Chat.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection _services)
        {
            _services.AddAutoMapper(u => u.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly())));
            _services.AddScoped<IChatRepository, ChatRepository>();

            return _services;
        }
    }
}
