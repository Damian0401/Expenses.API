using Application.Dtos.Module.Requests;
using Application.Dtos.Room.Requests;
using Application.Dtos.User.Requests;
using Application.Validators.Module;
using Application.Validators.Room;
using Application.Validators.User;
using FluentValidation;

namespace API.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserDtoRequest>, RegisterUserDtoRequestValidator>();
            services.AddScoped<IValidator<LoginUserDtoRequest>, LoginUserDtoRequestValidator>();
            services.AddScoped<IValidator<CreateModuleDtoRequest>, CreateModuleDtoRequestValidator>();
            services.AddScoped<IValidator<CreateRoomDtoRequest>, CreateRoomDtoRequestValidator>();

            return services;
        }
    }
}