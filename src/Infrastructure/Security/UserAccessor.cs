
using System.Linq;
using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetCurrentlyLoggedUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.Claims?
                .FirstOrDefault(x => x.Type.Equals("UserId"))?.Value;

            return userId;
        }
    }
}