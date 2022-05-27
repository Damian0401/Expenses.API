namespace Application.Dtos.User.Responses
{
    public class GetAllUsersDtoResponse
    {
        public ICollection<UserForGetAllUsersDtoResponse> Users { get; set; } = null!;
    }

    public class UserForGetAllUsersDtoResponse
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Guid? RoomId { get; set; }
        public ICollection<string> Roles { get; set; } = null!;
    }
}
