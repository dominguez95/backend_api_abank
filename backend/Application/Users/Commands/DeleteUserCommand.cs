using Application.DTOs;
using MediatR;

namespace Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<UserDto>
    {
        public Guid Id { get; set; }
    }
}