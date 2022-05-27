using Application.Dtos.Bill.Requests;
using Application.Dtos.Bill.Responses;
using Application.Services.Utilities;

namespace Application.Interfaces
{
    public interface IBillService
    {
        Task<ServiceResponse> CreateAsync(CreateBillDtoRequest dto);
        Task<ServiceResponse<GetAllBillsDtoResponse>> GetAllAsync();
        Task<ServiceResponse<GetRoomUnpaidBillsDtoResponse>> GetRoomUnpaidAsync();
        Task<ServiceResponse<GetRoomArchivedBillsDtoResponse>> GetRoomArchivedAsync();
        Task<ServiceResponse<GetMyUnpaidBillsDtoResponse>> GetMyUnpaidAsync();
        Task<ServiceResponse<GetMyArchivedBillsDtoResponse>> GetMyArchivedAsync();
        Task<ServiceResponse<GetBillByIdDtoRespose>> GetByIdAsync(Guid id);
        Task<ServiceResponse> ArchiveByIdAsync(Guid id);
        Task<ServiceResponse> ArchiveAllAsync();
    }
}
