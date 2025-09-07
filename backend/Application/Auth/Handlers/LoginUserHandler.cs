using Application.Auth.Commands;
using Application.Auth.Security;
using Application.Common.Security;
using Application.DTOs;
using Domain.Repositories;
using MediatR;

namespace Application.Auth.Handlers
{
    // Cambia la firma de la clase para que coincida con el tipo de respuesta esperado por MediatR
        public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtGenerator _jwtGenerator;

        public LoginHandler(IUserRepository userRepository, JwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.PhoneUser(request.Telefono);
            if (user == null) return null;

            // Validar contraseña encriptada
            if (!PasswordHasher.Verify(request.Password, user.Password))
                return null;

            var token = _jwtGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                User = user,
                AccessToken = token,
                TokenType = "bearer"
            };
        }
    }
}
