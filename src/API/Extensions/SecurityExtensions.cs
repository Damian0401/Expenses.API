using System.Text;
using Application.Interfaces;
using Application.Services;
using Domain.Models.Entities;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Presistence;

namespace API.Extensions
{
    public static class SecurityExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationSettings = new AuthenticationSettings();

            configuration.GetSection("Authentication").Bind(authenticationSettings);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.Key));
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = authenticationSettings.Audience,
                    ValidIssuer = authenticationSettings.Issuer,
                    IssuerSigningKey = key,
                };
            });

            services.AddIdentityCore<ApplicationUser>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddSingleton(authenticationSettings);

            return services;
        }
    }
}