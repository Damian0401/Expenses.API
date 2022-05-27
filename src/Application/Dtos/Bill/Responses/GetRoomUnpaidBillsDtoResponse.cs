namespace Application.Dtos.Bill.Responses
{
    public class GetRoomUnpaidBillsDtoResponse
    {
        public ICollection<BillForGetRoomUnpaidBillsDtoResponse> Bills { get; set; } = null!;
    }

    public class BillForGetRoomUnpaidBillsDtoResponse
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OwnerId { get; set; } = null!;
    }
}
