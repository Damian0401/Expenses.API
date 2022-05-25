using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Bill.Responses
{
    public class GetAllBillsDtoResponse
    {
        public ICollection<BillForGetAllBillsDtoResponse> Bills { get; set; } = null!;
    }

    public class BillForGetAllBillsDtoResponse
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public Status Status { get; set; }
        public string OwnerId { get; set; } = null!;
    }
}
