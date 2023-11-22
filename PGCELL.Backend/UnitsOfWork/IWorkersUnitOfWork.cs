using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public interface IWorkersUnitOfWork
    {
        Task<Response<Worker>> GetAsync(int id);
        Task<Response<IEnumerable<Worker>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<Worker>> GetComboAsync();
    }
}
