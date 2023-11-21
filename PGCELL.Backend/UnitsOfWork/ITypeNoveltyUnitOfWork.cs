using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public interface ITypeNoveltyUnitOfWork
    {
        Task<Response<IEnumerable<TypeNovelty>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<TypeNovelty>> GetComboAsync();
    }
}
