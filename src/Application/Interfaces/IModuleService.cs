using Application.Dtos.Module.Requests;
using Application.Dtos.Module.Responses;
using Application.Services.Utilities;

namespace Application.Interfaces
{
    public interface IModuleService
    {
        Task<ServiceResponse> CreateAsync(CreateModuleDtoRequest dto);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse<GetModuleByIdDtoResponse>> GetByIdAsync(Guid id);
        Task<ServiceResponse<GetAllModulesDtoResponse>> GetAllAsync();
    }
}
