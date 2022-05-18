using Application.Dtos.Room.Requests;
using Application.Dtos.Room.Responses;
using Application.Interfaces;
using Application.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoomService : Service, IRoomService
    {
        public RoomService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<ServiceResponse> CreateAsync(CreateRoomDtoRequest dto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetAllRoomsDtoResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetRoomByIdDtoResponse>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
