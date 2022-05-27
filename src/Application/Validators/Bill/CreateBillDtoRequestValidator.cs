using Application.Dtos.Bill.Requests;
using FluentValidation;

namespace Application.Validators.Bill
{
    public class CreateBillDtoRequestValidator : AbstractValidator<CreateBillDtoRequest>
    {
        public CreateBillDtoRequestValidator()
        {
            RuleFor(b => b.Value)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(b => b.Description)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
