using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User.Responses
{
    public class GetAllUsersDtoResponse
    {
        public ICollection<UserForGetAllUsersDtoResponse> Users { get; set; } = null!;
    }

    public class UserForGetAllUsersDtoResponse
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Guid? RoomId { get; set; }
        public ICollection<string> Roles { get; set; } = null!;
    }
}
