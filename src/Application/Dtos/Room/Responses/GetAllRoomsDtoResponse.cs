namespace Application.Dtos.Room.Responses
{
    public class GetAllRoomsDtoResponse
    {
        public ICollection<RoomForGetAllRoomsDtoResponse> Rooms { get; set; } = null!;
    }

    public class RoomForGetAllRoomsDtoResponse
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
    }
}
