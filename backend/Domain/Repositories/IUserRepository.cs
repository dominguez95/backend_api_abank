 using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<PagedResult<User>> GetUsers(int pageNumber, int pageSize = 10);
        Task<User?> GetUser(Guid id);
        Task CreateUser(User user);
        Task UpdateUser(User user, Guid id);
        Task DeleteUser(Guid id);
        Task<User?> PhoneUser(string telefono);

    }
}

