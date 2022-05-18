namespace Application.Dtos.User.Requests
{
    public class RegisterUserDtoRequest
    {
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid? RoomId { get; set; }
        public string Role { get; set; } = null!;
    }
}