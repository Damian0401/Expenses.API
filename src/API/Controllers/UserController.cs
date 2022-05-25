using System.Net;
using Application.Dtos.User.Requests;
using Application.Interfaces;
using Application.Services.Utilities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService accountService)
        {
            _userService = accountService;
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAllAsync();

            return SendResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDtoRequest dto)
        {
            var response = await _userService.RegisterAsync(dto);

            return SendResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDtoRequest dto)
        {
            var response = await _userService.LoginAsync(dto);

            return SendResponse(response);
        }
    }
}