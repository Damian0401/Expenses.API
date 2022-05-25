using Application.Dtos.Bill.Requests;
using Application.Dtos.Bill.Responses;
using Application.Interfaces;
using Application.Services.Utilities;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services
{
    public class BillService : Service, IBillService
    {
        public BillService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ServiceResponse> ArchiveAllAsync()
        {
            if (CurrentlyLoggedUser is null)
            {
                return new ServiceResponse(HttpStatusCode.Unauthorized);
            }

            var billsToArchive = await Context.Bills
                .Where(x => x.OwnerId.Equals(CurrentlyLoggedUserId) && x.Status == Status.Unpaid)
                .ToListAsync();

            foreach (var bill in billsToArchive)
            {
                bill.Status = Status.Archived;
            }

            var archiveResult = await Context.SaveChangesAsync();

            return archiveResult > 0
                ? new ServiceResponse(HttpStatusCode.OK)
                : new ServiceResponse(HttpStatusCode.BadRequest, "Unable to archive bills");
        }

        public async Task<ServiceResponse> ArchiveByIdAsync(Guid id)
        {
            var billToArchive = await Context.Bills
                .FirstOrDefaultAsync(b => b.Id.Equals(id));

            if (billToArchive is null)
            {
                return new ServiceResponse(HttpStatusCode.NotFound);
            }

            billToArchive.Status = Status.Archived;

            var archiveResult = await Context.SaveChangesAsync();

            return archiveResult > 0
                ? new ServiceResponse(HttpStatusCode.OK)
                : new ServiceResponse(HttpStatusCode.BadRequest, "Unable to archive bill");
        }

        public async Task<ServiceResponse> CreateAsync(CreateBillDtoRequest dto)
        {
            if (CurrentlyLoggedUser is null)
            {
                return new ServiceResponse(HttpStatusCode.Unauthorized);
            }

            var bill = Mapper.Map<Bill>(dto);

            bill.OwnerId = CurrentlyLoggedUser.Id;
            bill.Status = Status.Unpaid;
            bill.CreatedAt = DateTime.Now;

            Context.Bills.Add(bill);

            var addResult = await Context.SaveChangesAsync();

            return addResult > 0
                ? new ServiceResponse(HttpStatusCode.OK)
                : new ServiceResponse(HttpStatusCode.BadRequest, "Unable to create bill");
        }

        public async Task<ServiceResponse<GetAllBillsDtoResponse>> GetAllAsync()
        {
            var bills = await Context.Bills.ToListAsync();

            var billsDto = Mapper.Map<List<BillForGetAllBillsDtoResponse>>(bills);

            var responseDto = new GetAllBillsDtoResponse { Bills = billsDto };

            return new ServiceResponse<GetAllBillsDtoResponse>(HttpStatusCode.OK, responseDto);
        }

        public async Task<ServiceResponse<GetBillByIdDtoRespose>> GetByIdAsync(Guid id)
        {
            var bill = await Context.Bills
                .FirstOrDefaultAsync(b => b.Id.Equals(id));

            if (bill is null)
            {
                return new ServiceResponse<GetBillByIdDtoRespose>(HttpStatusCode.NotFound);
            }

            var dto = Mapper.Map<GetBillByIdDtoRespose>(bill);

            return new ServiceResponse<GetBillByIdDtoRespose>(HttpStatusCode.OK, dto);
        }

        public async Task<ServiceResponse<GetMyArchivedBillsDtoResponse>> GetMyArchivedAsync()
        {
            if (CurrentlyLoggedUser is null)
            {
                return new ServiceResponse<GetMyArchivedBillsDtoResponse>(HttpStatusCode.Unauthorized);
            }

            var unpaidBills = await Context.Bills
                .Where(x => x.OwnerId.Equals(CurrentlyLoggedUserId) && x.Status == Status.Archived)
                .ToListAsync();

            var unpaidBillsDto = Mapper.Map<List<BillForGetMyArchivedBillsDtoResponse>>(unpaidBills);

            var response = new GetMyArchivedBillsDtoResponse { Bills = unpaidBillsDto };

            return new ServiceResponse<GetMyArchivedBillsDtoResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetMyUnpaidBillsDtoResponse>> GetMyUnpaidAsync()
        {
            if (CurrentlyLoggedUser is null)
            {
                return new ServiceResponse<GetMyUnpaidBillsDtoResponse>(HttpStatusCode.Unauthorized);
            }

            var unpaidBills = await Context.Bills
                .Where(x => x.OwnerId.Equals(CurrentlyLoggedUserId) && x.Status == Status.Unpaid)
                .ToListAsync();

            var unpaidBillsDto = Mapper.Map<List<BillForGetMyUnpaidBillsDtoResponse>>(unpaidBills);

            var response = new GetMyUnpaidBillsDtoResponse { Bills = unpaidBillsDto };

            return new ServiceResponse<GetMyUnpaidBillsDtoResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetRoomArchivedBillsDtoResponse>> GetRoomArchivedAsync()
        {
            if (CurrentlyLoggedUser is null || CurrentlyLoggedUser.RoomId is null)
            {
                return new ServiceResponse<GetRoomArchivedBillsDtoResponse>(HttpStatusCode.BadRequest, "You need to have assigned roomId");
            }

            var archivedBills = await Context.Bills
                .Where(b => b.OwnerId.Equals(CurrentlyLoggedUser.Id) && b.Status.Equals(Status.Archived))
                .ToListAsync();

            var archivedBillsDto = Mapper.Map<List<BillForGetRoomArchivedBillsDtoResponse>>(archivedBills);

            var response = new GetRoomArchivedBillsDtoResponse { Bills = archivedBillsDto };

            return new ServiceResponse<GetRoomArchivedBillsDtoResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetRoomUnpaidBillsDtoResponse>> GetRoomUnpaidAsync()
        {
            if (CurrentlyLoggedUser is null || CurrentlyLoggedUser.RoomId is null)
            {
                return new ServiceResponse<GetRoomUnpaidBillsDtoResponse>(HttpStatusCode.BadRequest, "You need to have assigned roomId");
            }

            var unpaidBills = await Context.Bills
                .Where(b => b.OwnerId.Equals(CurrentlyLoggedUser.Id) && b.Status.Equals(Status.Unpaid))
                .ToListAsync();

            var unpaidBillsDto = Mapper.Map<List<BillForGetRoomUnpaidBillsDtoResponse>>(unpaidBills);

            var response = new GetRoomUnpaidBillsDtoResponse { Bills = unpaidBillsDto };

            return new ServiceResponse<GetRoomUnpaidBillsDtoResponse>(HttpStatusCode.OK, response);
        }
    }
}
