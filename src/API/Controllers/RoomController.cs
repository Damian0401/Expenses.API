using Application.Dtos.Room.Requests;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _roomService.GetAllAsync();

            return SendResponse(response);
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var response = await _roomService.GetByIdAsync(id);

            return SendResponse(response);
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateRoomDtoRequest dto)
        {
            var response = await _roomService.CreateAsync(dto);

            return SendResponse(response);
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _roomService.DeleteAsync(id);

            return SendResponse(response);
        }
    }
}
