using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.Repositories
{
    public interface IWorkSchedulesRepository
    {
        Task<Response<IEnumerable<WorkSchedule>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<WorkSchedule>> GetComboAsync();
    }
}
