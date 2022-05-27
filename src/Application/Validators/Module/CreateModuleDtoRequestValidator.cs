using Application.Dtos.Module.Requests;
using FluentValidation;
using Presistence;

namespace Application.Validators.Module
{
    public class CreateModuleDtoRequestValidator : AbstractValidator<CreateModuleDtoRequest>
    {
        public CreateModuleDtoRequestValidator(DataContext context)
        {
            var reservedModuleNames = context.Modules
                .Select(x => x.Name)
                .ToList();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255)
                .MinimumLength(5)
                .Must(n => !context.Modules.Any(m => m.Name.Equals(n)))
                .WithMessage("Module name have to be unique");
        }
    }
}
