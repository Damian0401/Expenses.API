using System.Net;
using Application.Dtos.User.Requests;
using Application.Dtos.User.Responses;
using Application.Interfaces;
using Application.Services.Utilities;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AccountService : Service, IAccountService
    {
        private readonly IJwtGenerator _jwtGenerator;

        public AccountService(IServiceProvider serviceProvider, IJwtGenerator jwtGenerator) : base(serviceProvider)
        {
            _jwtGenerator = jwtGenerator;
        }

        public async Task<ServiceResponse<LoginUserDtoResponse>> LoginAsync(LoginUserDtoRequest dto)
        {
            if (CurrentlyLoggedUser is not null)
            {
                return new ServiceResponse<LoginUserDtoResponse>(HttpStatusCode.BadRequest, "You are already logged in, log out first");
            }

            var userToLogin = await UserManager.FindByEmailAsync(dto.Email);

            if (userToLogin == null)
            {
                return new ServiceResponse<LoginUserDtoResponse>(HttpStatusCode.Unauthorized);
            }

            var isPasswordCorrect = await UserManager.CheckPasswordAsync(userToLogin, dto.Password);

            if (!isPasswordCorrect)
            {
                return new ServiceResponse<LoginUserDtoResponse>(HttpStatusCode.Unauthorized);
            }

            var token = await _jwtGenerator.CreateTokenAsync(userToLogin);
            var response = new LoginUserDtoResponse { Token = token };

            return new ServiceResponse<LoginUserDtoResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<RegisterUserDtoResponse>> RegisterAsync(RegisterUserDtoRequest dto)
        {
            if (dto.Role.Equals(Role.Administrator))
            {
                if (CurrentlyLoggedUser is null)
                {
                    return new ServiceResponse<RegisterUserDtoResponse>(HttpStatusCode.Unauthorized);
                }

                var isCurrentlyLoggedUserAdmin = await UserManager.IsInRoleAsync(CurrentlyLoggedUser, Role.Administrator);

                if (!isCurrentlyLoggedUserAdmin)
                {
                    return new ServiceResponse<RegisterUserDtoResponse>(HttpStatusCode.Forbidden);
                }
            }

            var userToRegister = Mapper.Map<ApplicationUser>(dto);
            var createResult = await UserManager.CreateAsync(userToRegister, dto.Password);

            if (!createResult.Equals(IdentityResult.Success))
            {
                var errors = string.Join("\n", createResult.Errors.Select(x => x.Description));

                return new ServiceResponse<RegisterUserDtoResponse>(HttpStatusCode.BadRequest, errors);
            }

            await UserManager.AddToRoleAsync(userToRegister, dto.Role);
            var token = await _jwtGenerator.CreateTokenAsync(userToRegister);
            var response = new RegisterUserDtoResponse { Token = token };

            return new ServiceResponse<RegisterUserDtoResponse>(HttpStatusCode.OK, response);
        }
    }
}