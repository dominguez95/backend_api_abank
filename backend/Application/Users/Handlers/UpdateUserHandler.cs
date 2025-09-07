using Application.DTOs;
using Application.Users.Commands;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _repository;

        public UpdateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
     
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                Email = request.Email,
                Password = request.Password,
                Fecha_nacimiento = request.Fecha_nacimiento,
                Telefono = request.Telefono,
                Direccion = request.Direccion,
                Estado = request.Estado
            };

            await _repository.UpdateUser(user,  request.Id);

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