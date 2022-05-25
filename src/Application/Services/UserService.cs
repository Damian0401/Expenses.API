using System.Net;
using Application.Dtos.User.Requests;
using Application.Dtos.User.Responses;
using Application.Interfaces;
using Application.Services.Utilities;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService : Service, IUserService
    {
        private readonly IJwtGenerator _jwtGenerator;

        public UserService(IServiceProvider serviceProvider, IJwtGenerator jwtGenerator) : base(serviceProvider)
        {
            _jwtGenerator = jwtGenerator;
        }

        public async Task<ServiceResponse<GetAllUsersDtoResponse>> GetAllAsync()
        {
            var users = await Context.Users.ToListAsync();

            var usersDto = new List<UserForGetAllUsersDtoResponse>();

            foreach (var user in users)
            {
                var dto = Mapper.Map<UserForGetAllUsersDtoResponse>(user);

                dto.Roles = await UserManager.GetRolesAsync(user);

                usersDto.Add(dto);
            }

            var response = new GetAllUsersDtoResponse { Users = usersDto };

            return new ServiceResponse<GetAllUsersDtoResponse>(HttpStatusCode.OK, response);

        }

        public async Task<ServiceResponse<GetUserExpensesDtoResponse>> GetExpensesAsync()
        {
            if (CurrentlyLoggedUser is null)
            {
                return new ServiceResponse<GetUserExpensesDtoResponse>(HttpStatusCode.Unauthorized);
            }

            var userRoom = await Context.Rooms
                .Include(r => r.Residents)
                .FirstOrDefaultAsync(r => r.Id.Equals(CurrentlyLoggedUser.RoomId));

            if (userRoom is null)
            {
                return new ServiceResponse<GetUserExpensesDtoResponse>(HttpStatusCode.BadRequest);
            }

            var response = await GetExponsesListAsync(userRoom);

            return new ServiceResponse<GetUserExpensesDtoResponse>(HttpStatusCode.OK, response);
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
            var roleValidationStatus = await ValidateUserRoleAsync(dto);

            if (roleValidationStatus != HttpStatusCode.OK)
            {
                return new ServiceResponse<RegisterUserDtoResponse>(roleValidationStatus);
            }

            var userToRegister = Mapper.Map<ApplicationUser>(dto);

            var isRoomIdValid = await ValidateRoomIdAsync(dto);

            if (isRoomIdValid == false)
            {
                return new ServiceResponse<RegisterUserDtoResponse>(HttpStatusCode.BadRequest, "Invalid roomId");
            }

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

        private async Task<HttpStatusCode> ValidateUserRoleAsync(RegisterUserDtoRequest user)
        {
            if (user.Role.Equals(Role.Administrator))
            {
                if (CurrentlyLoggedUser is null)
                {
                    return HttpStatusCode.Unauthorized;
                }

                var isCurrentlyLoggedUserAdmin = await UserManager.IsInRoleAsync(CurrentlyLoggedUser, Role.Administrator);

                if (!isCurrentlyLoggedUserAdmin)
                {
                    return HttpStatusCode.Forbidden;
                }
            }

            return HttpStatusCode.OK;
        }

        private async Task<bool> ValidateRoomIdAsync(RegisterUserDtoRequest user)
        {
            if (user.RoomId is null && !user.Role.Equals(Role.Administrator))
            {
                return false;
            }

            if (user.RoomId is not null)
            {
                var userRoom = await Context.Rooms
                    .Include(r => r.Residents)
                    .FirstOrDefaultAsync(r => r.Id.Equals(user.RoomId));

                if (userRoom is null || userRoom.Residents.Count >= userRoom.MaxResidentNumber)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<GetUserExpensesDtoResponse> GetExponsesListAsync(Room userRoom)
        {
            var residentsId = userRoom.Residents
                .Select(r => r.Id)
                .Where(id => id != CurrentlyLoggedUser!.Id);

            var billsToPay = await Context.Bills
                .Where(b => residentsId.Contains(b.OwnerId) && b.Status.Equals(Status.Unpaid))
                .ToListAsync();

            var userExpenses = new List<ExpenseForGetUserExponsesDtoResponse>();

            foreach (var resident in userRoom.Residents)
            {
                var residentBills = billsToPay.Where(b => b.OwnerId.Equals(resident.Id));

                if (residentBills.Count() == 0)
                {
                    continue;
                }

                var expense = new ExpenseForGetUserExponsesDtoResponse
                {
                    OwnerName = $"{resident.FirstName} {resident.LastName}",
                    Value = residentBills.Sum(b => b.Value) / (residentsId.Count() + 1)
                };

                userExpenses.Add(expense);
            }

            var response = new GetUserExpensesDtoResponse
            {
                TotalValue = userExpenses.Sum(x => x.Value),
                Expenses = userExpenses
            };

            return response;
        }
    }
}