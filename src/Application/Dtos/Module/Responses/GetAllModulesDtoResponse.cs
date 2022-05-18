using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Module.Responses
{
    public class GetAllModulesDtoResponse
    {
        public ICollection<ModuleForGetAllModulesDtoResponse> Modules { get; set; } = null!;
    }

    public class ModuleForGetAllModulesDtoResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
