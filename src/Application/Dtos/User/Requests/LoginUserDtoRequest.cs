namespace Application.Dtos.User.Requests
{
    public class LoginUserDtoRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}