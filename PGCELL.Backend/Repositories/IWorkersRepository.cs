using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.Repositories
{
    public interface IWorkersRepository
    {
        Task<Response<Worker>> GetAsync(int id);

        Task<Response<IEnumerable<Worker>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<Worker>> GetComboAsync();
        Task<IEnumerable<Worker>> GetActiveWorkersAsync();
    }
}
