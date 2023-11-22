using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public class NoveltiesUnitOfWork : GenericUnitOfWork<Novelty>, INoveltiesUnitOfWork
    {
        private readonly INoveltiesRepository _noveltiesRepository;

        public NoveltiesUnitOfWork(IGenericRepository<Novelty> repository, INoveltiesRepository noveltiesRepository) : base(repository)
        {
            _noveltiesRepository = noveltiesRepository;
        }

        public override async Task<Response<IEnumerable<Novelty>>> GetAsync(PaginationDTO pagination) => await _noveltiesRepository.GetAsync(pagination);

        public async Task<IEnumerable<Novelty>> GetComboAsync() => await _noveltiesRepository.GetComboAsync();

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _noveltiesRepository.GetTotalPagesAsync(pagination);
    }
}
