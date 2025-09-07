
using Application.Auth.Commands;
using FluentValidation;

namespace Application.Auth.Validators
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Telefono).NotEmpty().WithMessage("Teléfono es obligatorio");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Contraseña es obligatoria");
        }
    }
}
