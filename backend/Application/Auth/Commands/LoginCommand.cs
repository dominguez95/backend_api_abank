using Application.DTOs;
using MediatR;

namespace Application.Auth.Commands
{
    public class LoginCommand : IRequest<AuthResponseDto>
    {
        public string Telefono { get; set; }
        public string Password { get; set; }
    }
}
