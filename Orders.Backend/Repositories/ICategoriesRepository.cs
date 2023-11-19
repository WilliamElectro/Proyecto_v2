using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.Repositories
{
    public interface ICategoriesRepository
    {
        Task<Response<IEnumerable<Category>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<Category>> GetComboAsync();
    }
}