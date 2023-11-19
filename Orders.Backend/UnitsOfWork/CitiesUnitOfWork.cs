using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public class CitiesUnitOfWork : GenericUnitOfWork<City>, ICitiesUnitOfWork
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesUnitOfWork(IGenericRepository<City> repository, ICitiesRepository citiesRepository) : base(repository)
        {
            _citiesRepository = citiesRepository;
        }

        public override async Task<Response<IEnumerable<City>>> GetAsync(PaginationDTO pagination) => await _citiesRepository.GetAsync(pagination);

        public async Task<IEnumerable<City>> GetComboAsync(int stateId) => await _citiesRepository.GetComboAsync(stateId);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _citiesRepository.GetTotalPagesAsync(pagination);
    }
}