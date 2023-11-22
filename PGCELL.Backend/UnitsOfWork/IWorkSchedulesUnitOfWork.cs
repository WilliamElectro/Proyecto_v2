using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public interface IWorkSchedulesUnitOfWork
    {
        Task<Response<IEnumerable<WorkSchedule>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<WorkSchedule>> GetComboAsync();
    }
}
