namespace Application.Dtos.User.Responses
{
    public class GetUserExpensesDtoResponse
    {
        public double TotalValue { get; set; }
        public ICollection<ExpenseForGetUserExponsesDtoResponse> Expenses { get; set; } = null!;
    }

    public class ExpenseForGetUserExponsesDtoResponse
    {
        public string OwnerName { get; set; } = null!;
        public double Value { get; set; }
    }
}
