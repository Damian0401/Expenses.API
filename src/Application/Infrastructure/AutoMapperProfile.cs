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
            MapsForRoom();
            MapsForModule();
        }

        private void MapForUser()
        {
            CreateMap<RegisterUserDtoRequest, ApplicationUser>();
        }

        private void MapsForRoom()
        {
            CreateMap<Room, RoomForGetModuleByIdDtoResponse>();
        }

        private void MapsForModule()
        {
            CreateMap<CreateModuleDtoRequest, Module>();
            CreateMap<Module, ModuleForGetAllModulesDtoResponse>();
            CreateMap<Module, GetModuleByIdDtoResponse>();
        }
    }
}