namespace Application.Dtos.Room.Requests
{
    public class CreateRoomDtoRequest
    {
        public int Number { get; set; }
        public Guid ModuleId { get; set; }
        public int MaxResidentNumber { get; set; }
    }
}
