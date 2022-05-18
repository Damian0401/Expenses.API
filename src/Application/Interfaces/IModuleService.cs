using Application.Dtos.Module.Requests;
using Application.Dtos.Module.Responses;
using Application.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IModuleService
    {
        Task<ServiceResponse> CreateAsync(CreateModuleDtoRequest dto);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse<GetModuleByIdDtoResponse>> GetById(Guid id);
        Task<ServiceResponse<GetAllModulesDtoResponse>> GetAll();
    }
}
