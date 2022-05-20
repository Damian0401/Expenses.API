using Application.Dtos.Room.Requests;
using Application.Dtos.Room.Responses;
using Application.Interfaces;
using Application.Services.Utilities;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoomService : Service, IRoomService
    {
        public RoomService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ServiceResponse> CreateAsync(CreateRoomDtoRequest dto)
        {
            var roomToAdd = Mapper.Map<Room>(dto);

            Context.Rooms.Add(roomToAdd);

            var addResult = await Context.SaveChangesAsync();

            return addResult > 0
                ? new ServiceResponse(HttpStatusCode.OK)
                : new ServiceResponse(HttpStatusCode.BadRequest, "Unable to create room");
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
