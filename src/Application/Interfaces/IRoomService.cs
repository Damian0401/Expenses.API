using Application.Dtos.Room.Requests;
using Application.Dtos.Room.Responses;
using Application.Services.Utilities;

namespace Application.Interfaces
{
    public interface IRoomService
    {
        Task<ServiceResponse> CreateAsync(CreateRoomDtoRequest dto);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse<GetAllRoomsDtoResponse>> GetAllAsync();
        Task<ServiceResponse<GetRoomByIdDtoResponse>> GetByIdAsync(Guid id);
    }
}
