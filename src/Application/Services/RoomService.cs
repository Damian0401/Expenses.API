using Application.Dtos.Room.Requests;
using Application.Dtos.Room.Responses;
using Application.Interfaces;
using Application.Services.Utilities;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var roomToDelete = await Context.Rooms
                .FirstOrDefaultAsync(r => r.Id.Equals(id));

            if (roomToDelete is null)
            {
                return new ServiceResponse(HttpStatusCode.NotFound);
            }

            Context.Rooms.Remove(roomToDelete);

            await Context.SaveChangesAsync();

            return new ServiceResponse(HttpStatusCode.OK);
        }

        public async Task<ServiceResponse<GetAllRoomsDtoResponse>> GetAllAsync()
        {
            var rooms = await Context.Rooms.ToListAsync();

            var roomsDto = Mapper.Map<List<RoomForGetAllRoomsDtoResponse>>(rooms);

            var response = new GetAllRoomsDtoResponse { Rooms = roomsDto };

            return new ServiceResponse<GetAllRoomsDtoResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetRoomByIdDtoResponse>> GetByIdAsync(Guid id)
        {
            var room = await Context.Rooms
                .FirstOrDefaultAsync(r => r.Id.Equals(id));

            if (room is null)
            {
                return new ServiceResponse<GetRoomByIdDtoResponse>(HttpStatusCode.NotFound);
            }

            var dto = Mapper.Map<GetRoomByIdDtoResponse>(room);

            return new ServiceResponse<GetRoomByIdDtoResponse>(HttpStatusCode.OK, dto);
        }
    }
}
