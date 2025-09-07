using Application.DTOs;
using Application.Users.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Queries
{
    public class GetUserByIdQuery : IRequestHandler<GetUserByIdCommand, UserDto>
    {
        private readonly IUserRepository _repository;

        public GetUserByIdQuery(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<UserDto> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUser(request.Id);
            if (user == null) return null;

            return new UserDto
            {
                Nombres = user.Nombres,
                Apellidos = user.Apellidos,
                Email = user.Email,
                Telefono = user.Telefono,
                Direccion = user.Direccion,
                Estado = user.Estado,
                Fecha_nacimiento = user.Fecha_nacimiento
            };
        }
    }
}