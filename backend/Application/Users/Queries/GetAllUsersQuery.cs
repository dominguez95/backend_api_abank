using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Queries
{
    public record GetAllUsersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PagedResult<User>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResult<User>>

    {
        private readonly IUserRepository _repository;

        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            return await _repository.GetUsers(pageNumber, pageSize);
        }
    }
}