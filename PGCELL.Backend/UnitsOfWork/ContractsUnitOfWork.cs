using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PGCELL.Backend.UnitsOfWork
{
    public class ContractsUnitOfWork : GenericUnitOfWork<Contract>, IContractsUnitOfWork
    {
        private readonly IContractsRepository _contractsRepository;

        public ContractsUnitOfWork(IGenericRepository<Contract> repository, IContractsRepository contractsRepository) : base(repository)
        {
            _contractsRepository = contractsRepository;
        }

        public override async Task<Response<IEnumerable<Contract>>> GetAsync(PaginationDTO pagination) => await _contractsRepository.GetAsync(pagination);

        // Aquí deberías implementar los métodos específicos para la entidad Contract
        // Por ejemplo:

        public async Task<IEnumerable<Contract>> GetComboAsync() => await _contractsRepository.GetComboAsync();

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _contractsRepository.GetTotalPagesAsync(pagination);
    }
}
