using AutoMapper;
using System.Reflection;

namespace Portfol.io.Chat.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            ApplyMappingsFromAssembly(assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(u => u.GetInterfaces()
                .Any(p => p.IsGenericType && p.GetGenericTypeDefinition() == typeof(IMapWith<>))).ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
