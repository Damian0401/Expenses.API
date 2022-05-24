using Application.Dtos.Room.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Presistence;

namespace Application.Validators.Room
{
    public class CreateRoomDtoRequestValidator : AbstractValidator<CreateRoomDtoRequest>
    {
        public CreateRoomDtoRequestValidator(DataContext context)
        {
            RuleFor(x => x.ModuleId)
                .NotEmpty()
                .Must(x => context.Modules.Any(m => m.Id.Equals(x)))
                .WithMessage("ModuleId have to match to any existing module");

            RuleFor(x => x.MaxResidentNumber)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Number)
                .NotEmpty()
                .Must(x => !context.Rooms.Any(r => r.Number.Equals(x)))
                .WithMessage("Room name have to be unique");
        }
    }
}