using Application.Dtos.Module.Requests;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class ModuleController : BaseController
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _moduleService.GetAll();

            return SendResponse(response);
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var response = await _moduleService.GetById(id);

            return SendResponse(response);
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateModuleDtoRequest dto)
        {
            var response = await _moduleService.CreateAsync(dto);

            return SendResponse(response);
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _moduleService.DeleteAsync(id);

            return SendResponse(response);
        }
    }
}
