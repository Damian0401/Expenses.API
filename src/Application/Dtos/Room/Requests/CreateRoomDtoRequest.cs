using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Room.Requests
{
    public class CreateRoomDtoRequest
    {
        public int Number { get; set; }
        public Guid ModuleId { get; set; }
        public int MaxResidentNumber { get; set; }
    }
}
