using Application.Dtos.Bill.Requests;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BillController : BaseController
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _billService.GetAllAsync();

            return SendResponse(response);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var response = await _billService.GetByIdAsync(id);

            return SendResponse(response);
        }

        [Authorize]
        [HttpGet("My/Unpaid")]
        public async Task<IActionResult> GetUnpaid()
        {
            var response = await _billService.GetMyUnpaidAsync();

            return SendResponse(response);
        }

        [Authorize]
        [HttpGet("My/Archive")]
        public async Task<IActionResult> GetArchived()
        {
            var response = await _billService.GetMyArchivedAsync();

            return SendResponse(response);
        }

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateBillDtoRequest dto)
        {
            var response = await _billService.CreateAsync(dto);

            return SendResponse(response);
        }

        [Authorize]
        [HttpPost("My/Archive")]
        public async Task<IActionResult> ArchiveAll()
        {
            var response = await _billService.ArchiveAllAsync();

            return SendResponse(response);
        }

        [Authorize]
        [HttpPost("My/Archive/{id:guid}")]
        public async Task<IActionResult> Archive([FromRoute] Guid id)
        {
            var response = await _billService.ArchiveByIdAsync(id);

            return SendResponse(response);
        }
    }
}
