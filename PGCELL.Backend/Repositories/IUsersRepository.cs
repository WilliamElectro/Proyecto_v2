using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.Repositories
{
    public interface IUsersRepository
    {
        Task<Response<User>> GetAsync(string email);

        Task<Response<User>> GetAsync(Guid userId);

        Task<Response<IEnumerable<User>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}