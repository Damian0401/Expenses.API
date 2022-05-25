using Application.Dtos.User.Requests;
using Application.Dtos.User.Responses;
using Application.Services.Utilities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse<LoginUserDtoResponse>> LoginAsync(LoginUserDtoRequest dto);
        public Task<ServiceResponse<RegisterUserDtoResponse>> RegisterAsync(RegisterUserDtoRequest dto);
        public Task<ServiceResponse<GetAllUsersDtoResponse>> GetAllAsync();
        public Task<ServiceResponse<GetUserExpensesDtoResponse>> GetExpensesAsync();
    }
}