namespace Application.Dtos.Bill.Responses
{
    public class GetMyArchivedBillsDtoResponse
    {
        public ICollection<BillForGetMyArchivedBillsDtoResponse> Bills { get; set; } = null!;
    }

    public class BillForGetMyArchivedBillsDtoResponse
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
