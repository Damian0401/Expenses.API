using Application.Interfaces;
using Application.Services;

namespace Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBillService, BillService>();

            return services;
        }
    }
}