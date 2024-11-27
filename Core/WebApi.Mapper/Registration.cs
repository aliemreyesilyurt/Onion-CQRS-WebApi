using Microsoft.Extensions.DependencyInjection;
using WebApi.Application.Interfaces.AutoMapper;

namespace WebApi.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}
