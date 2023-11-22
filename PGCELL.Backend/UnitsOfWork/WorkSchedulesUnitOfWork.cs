using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public class WorkSchedulesUnitOfWork : GenericUnitOfWork<WorkSchedule>, IWorkSchedulesUnitOfWork
    {
        private readonly IWorkSchedulesRepository _workSchedulesRepository;

        public WorkSchedulesUnitOfWork(IGenericRepository<WorkSchedule> repository, IWorkSchedulesRepository workSchedulesRepository) : base(repository)
        {
            _workSchedulesRepository = workSchedulesRepository;
        }

        public override async Task<Response<IEnumerable<WorkSchedule>>> GetAsync(PaginationDTO pagination) => await _workSchedulesRepository.GetAsync(pagination);

        public async Task<IEnumerable<WorkSchedule>> GetComboAsync() => await _workSchedulesRepository.GetComboAsync();

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _workSchedulesRepository.GetTotalPagesAsync(pagination);
    }
}
