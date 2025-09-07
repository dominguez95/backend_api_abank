using FluentValidation;
using Application.Users.Commands;

namespace Application.Users.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Nombres).NotEmpty().WithMessage("Nombres es obligatorio");
            RuleFor(x => x.Apellidos).NotEmpty().WithMessage("Apellidos es obligatorio");
            RuleFor(x => x.Fecha_nacimiento)
                .NotEmpty()
                .Must(date => date != DateOnly.MinValue)
                .WithMessage("Fecha_nacimiento debe ser una fecha válida en formato YYYY-MM-DD");
            RuleFor(x => x.Telefono).NotEmpty().WithMessage("Teléfono es obligatorio");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.Direccion).NotEmpty().WithMessage("Dirección es obligatorio");
            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("Estado es obligatorio")
                .Must(e => e == "A" || e == "I")
                .WithMessage("Estado solo puede ser 'A' (Activo) o 'I' (Inactivo)");
        }
    }
}
