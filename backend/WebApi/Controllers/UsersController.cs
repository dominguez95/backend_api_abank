using Application.DTOs;
using Application.Users.Commands;
using Application.Users.Queries;

using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [Produces("application/json")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        // GET api/users?page=1&size=10
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<User>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<User>>> GetAllUsers([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _mediator.Send(new GetAllUsersQuery(page, size));
            return Ok(result);
        }
        // GET api/users/{id_user}
        [HttpGet("{id_user:guid}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] Guid id_user)
        {
            var result = await _mediator.Send(new GetUserByIdCommand { Id = id_user });
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST api/users
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserCommand request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        // PUT api/users/{id_user}
        [HttpPut("{id_user:guid}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> UpdateUser([FromRoute] Guid id_user, [FromBody] CreateUserCommand  request)
        {
            var bodyRequest = new UpdateUserCommand();
            bodyRequest.Id = id_user;
            bodyRequest.Nombres = request.Nombres;
            bodyRequest.Apellidos = request.Apellidos;
            bodyRequest.Email = request.Email;
            bodyRequest.Password = request.Password;
            bodyRequest.Estado = request.Estado;
            bodyRequest.Fecha_nacimiento = request.Fecha_nacimiento;
            bodyRequest.Direccion = request.Direccion;
            bodyRequest.Telefono = request.Telefono;

            var result = await _mediator.Send(bodyRequest);
            return Ok(result);
        }

        // DELETE api/users/{id_user}
        [HttpDelete("{id_user:guid}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id_user)
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id_user });
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
