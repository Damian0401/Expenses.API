using Application.Dtos.Module.Requests;
using Application.Dtos.Module.Responses;
using Application.Interfaces;
using Application.Services.Utilities;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services
{
    public class ModuleService : Service, IModuleService
    {
        public ModuleService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ServiceResponse> CreateAsync(CreateModuleDtoRequest dto)
        {
            var moduleToAdd = Mapper.Map<Module>(dto);

            Context.Modules.Add(moduleToAdd);

            var addResult = await Context.SaveChangesAsync();

            return addResult > 0
                ? new ServiceResponse(HttpStatusCode.OK)
                : new ServiceResponse(HttpStatusCode.BadRequest, "Unable to create module");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var moduleToDelete = await Context.Modules.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (moduleToDelete is null)
            {
                return new ServiceResponse(HttpStatusCode.NotFound);
            }

            Context.Modules.Remove(moduleToDelete);

            await Context.SaveChangesAsync();

            return new ServiceResponse(HttpStatusCode.OK);

        }

        public async Task<ServiceResponse<GetAllModulesDtoResponse>> GetAllAsync()
        {
            var modules = await Context.Modules.ToListAsync();

            var modulesDto = Mapper.Map<List<ModuleForGetAllModulesDtoResponse>>(modules);

            var response = new GetAllModulesDtoResponse { Modules = modulesDto };

            return new ServiceResponse<GetAllModulesDtoResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetModuleByIdDtoResponse>> GetByIdAsync(Guid id)
        {
            var module = await Context.Modules
                .Include(x => x.Rooms)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (module is null)
            {
                return new ServiceResponse<GetModuleByIdDtoResponse>(HttpStatusCode.NotFound);
            }

            var dto = Mapper.Map<GetModuleByIdDtoResponse>(module);

            return new ServiceResponse<GetModuleByIdDtoResponse>(HttpStatusCode.OK, dto);
        }
    }
}
