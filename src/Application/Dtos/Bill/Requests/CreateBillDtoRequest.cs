using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Bill.Requests
{
    public class CreateBillDtoRequest
    {
        public double Value { get; set; }
        public string Description { get; set; } = null!;
    }
}
