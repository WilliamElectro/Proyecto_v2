using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public class StatesUnitOfWork : GenericUnitOfWork<State>, IStatesUnitOfWork
    {
        private readonly IStatesRepository _statesRepository;

        public StatesUnitOfWork(IGenericRepository<State> repository, IStatesRepository statesRepository) : base(repository)
        {
            _statesRepository = statesRepository;
        }

        public override async Task<Response<IEnumerable<State>>> GetAsync(PaginationDTO pagination) => await _statesRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _statesRepository.GetTotalPagesAsync(pagination);

        public override async Task<Response<State>> GetAsync(int id) => await _statesRepository.GetAsync(id);

        public async Task<IEnumerable<State>> GetComboAsync(int countryId) => await _statesRepository.GetComboAsync(countryId);
    }
}