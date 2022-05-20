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
                .NotEmpty();

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