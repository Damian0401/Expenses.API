namespace Application.Dtos.Module.Responses
{
    public class GetModuleByIdDtoResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<RoomForGetModuleByIdDtoResponse> Rooms { get; set; } = null!;
    }

    public class RoomForGetModuleByIdDtoResponse
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
    }
}
