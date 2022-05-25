using Application.Dtos.Bill.Requests;
using Application.Dtos.Bill.Responses;
using Application.Dtos.Module.Requests;
using Application.Dtos.Module.Responses;
using Application.Dtos.Room.Requests;
using Application.Dtos.Room.Responses;
using Application.Dtos.User.Requests;
using Application.Dtos.User.Responses;
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
            MapsForBill();
        }

        private void MapForUser()
        {
            CreateMap<RegisterUserDtoRequest, ApplicationUser>();
            CreateMap<ApplicationUser, UserForGetAllUsersDtoResponse>();
        }

        private void MapsForRoom()
        {
            CreateMap<CreateRoomDtoRequest, Room>();
            CreateMap<Room, RoomForGetAllRoomsDtoResponse>();
            CreateMap<Room, GetRoomByIdDtoResponse>();
            CreateMap<Room, RoomForGetModuleByIdDtoResponse>();
        }

        private void MapsForModule()
        {
            CreateMap<CreateModuleDtoRequest, Module>();
            CreateMap<Module, ModuleForGetAllModulesDtoResponse>();
            CreateMap<Module, GetModuleByIdDtoResponse>();
        }

        private void MapsForBill()
        {
            CreateMap<Bill, BillForGetMyUnpaidBillsDtoResponse>();
            CreateMap<Bill, BillForGetAllBillsDtoResponse>();
            CreateMap<Bill, BillForGetMyArchivedBillsDtoResponse>();
            CreateMap<Bill, BillForGetRoomUnpaidBillsDtoResponse>();
            CreateMap<Bill, BillForGetRoomArchivedBillsDtoResponse>();
            CreateMap<Bill, GetBillByIdDtoRespose>();
            CreateMap<CreateBillDtoRequest, Bill>();
        }
    }
}