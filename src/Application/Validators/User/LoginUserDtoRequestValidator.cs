using Application.Dtos.User.Requests;
using FluentValidation;

namespace Application.Validators.User
{
    public class LoginUserDtoRequestValidator : AbstractValidator<LoginUserDtoRequest>
    {
        public LoginUserDtoRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}