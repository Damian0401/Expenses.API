namespace Application.Dtos.Room.Responses
{
    public class GetRoomByIdDtoResponse
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid ModuleId { get; set; }
        public int MaxResidentNumber { get; set; }
    }
}
