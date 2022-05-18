using Application.Dtos.User.Requests;
using Application.Dtos.User.Responses;
using Application.Services.Utilities;

namespace Application.Interfaces
{
    public interface IAccountService
    {
         public Task<ServiceResponse<LoginUserDtoResponse>> LoginAsync(LoginUserDtoRequest dto);
         public Task<ServiceResponse<RegisterUserDtoResponse>> RegisterAsync(RegisterUserDtoRequest dto);
    }
}