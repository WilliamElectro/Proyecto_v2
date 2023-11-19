using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public interface ICategoriesUnitOfWork
    {
        Task<Response<IEnumerable<Category>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<Category>> GetComboAsync();
    }
}