using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public interface ICountriesUnitOfWork
    {
        Task<Response<Country>> GetAsync(int id);

        Task<Response<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<Country>> GetComboAsync();
    }
}