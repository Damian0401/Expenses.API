using System.Net;
using Application.Dtos.User.Requests;
using Application.Interfaces;
using Application.Services.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDtoRequest dto)
        {
            var response = await _accountService.RegisterAsync(dto);

            return SendResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDtoRequest dto)
        {
            var response = await _accountService.LoginAsync(dto);

            return SendResponse(response);
        }
    }
}