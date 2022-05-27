namespace Application.Dtos.Bill.Requests
{
    public class CreateBillDtoRequest
    {
        public double Value { get; set; }
        public string Description { get; set; } = null!;
    }
}
