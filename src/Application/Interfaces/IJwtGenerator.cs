using Domain.Models.Entities;

namespace Application.Interfaces
{
    public interface IJwtGenerator
    {
        Task<string> CreateTokenAsync(ApplicationUser user);
    }
}