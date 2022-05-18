using Application.Dtos.User.Requests;
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

            return services;
        }
    }
}