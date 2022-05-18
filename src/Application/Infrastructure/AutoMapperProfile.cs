using Application.Dtos.Module.Requests;
using Application.Dtos.Module.Responses;
using Application.Dtos.User.Requests;
using AutoMapper;
using Domain.Models.Entities;

namespace Application.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapForUser();
            MapsForModule();
        }

        private void MapForUser()
        {
            CreateMap<RegisterUserDtoRequest, ApplicationUser>();
        }

        private void MapsForModule()
        {
            CreateMap<CreateModuleDtoRequest, Module>();
            CreateMap<Module, GetModuleByIdDtoResponse>();
            CreateMap<Module, ModuleForGetAllModulesDtoResponse>();
        }
    }
}