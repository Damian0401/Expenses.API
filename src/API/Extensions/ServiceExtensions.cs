using Application.Interfaces;
using Application.Services;
using Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IRoomService, RoomService>();

            return services;
        }
    }
}