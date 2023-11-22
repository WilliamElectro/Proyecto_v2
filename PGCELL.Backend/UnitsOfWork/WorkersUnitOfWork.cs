using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PGCELL.Backend.UnitsOfWork
{
    public class WorkersUnitOfWork : GenericUnitOfWork<Worker>, IWorkersUnitOfWork
    {
        private readonly IWorkersRepository _workersRepository;

        public WorkersUnitOfWork(IGenericRepository<Worker> repository, IWorkersRepository workersRepository) : base(repository)
        {
            _workersRepository = workersRepository;
        }

        public override async Task<Response<IEnumerable<Worker>>> GetAsync(PaginationDTO pagination) => await _workersRepository.GetAsync(pagination);

        // Aquí deberías implementar los métodos específicos para la entidad Worker
        // Por ejemplo:

        public async Task<IEnumerable<Worker>> GetComboAsync() => await _workersRepository.GetComboAsync();

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _workersRepository.GetTotalPagesAsync(pagination);
    }
}
