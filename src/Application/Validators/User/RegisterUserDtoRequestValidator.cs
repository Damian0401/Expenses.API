using Application.Dtos.User.Requests;
using Domain.Models;
using FluentValidation;

namespace Application.Validators.User
{
    public class RegisterUserDtoRequestValidator : AbstractValidator<RegisterUserDtoRequest>
    {
        public RegisterUserDtoRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(255)
                .MinimumLength(5);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(255)
                .MinimumLength(5);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255)
                .MinimumLength(5);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(255)
                .MinimumLength(5);

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(x => Role.AllRoles.Contains(x!))
                .WithMessage($"Role must be in [{string.Join(",", Role.AllRoles)}]");
        }
    }
}