namespace Application.Dtos.Bill.Responses
{
    public class GetRoomArchivedBillsDtoResponse
    {
        public ICollection<BillForGetRoomArchivedBillsDtoResponse> Bills { get; set; } = null!;
    }

    public class BillForGetRoomArchivedBillsDtoResponse
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OwnerId { get; set; } = null!;
    }
}
