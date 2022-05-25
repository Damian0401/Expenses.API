using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Bill.Responses
{
    public class GetMyUnpaidBillsDtoResponse
    {
        public ICollection<BillForGetMyUnpaidBillsDtoResponse> Bills { get; set; } = null!;
    }

    public class BillForGetMyUnpaidBillsDtoResponse
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
