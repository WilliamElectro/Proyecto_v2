using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.UnitsOfWork
{
    public class ModalitiesUnitOfWork : GenericUnitOfWork<Modality>, IModalitiesUnitOfWork
    {
        private readonly IModalitiesRepository _modalitiesRepository;

        public ModalitiesUnitOfWork(IGenericRepository<Modality> repository, IModalitiesRepository modalitiesRepository) : base(repository)
        {
            _modalitiesRepository = modalitiesRepository;
        }

        public override async Task<Response<IEnumerable<Modality>>> GetAsync(PaginationDTO pagination) => await _modalitiesRepository.GetAsync(pagination);

        public async Task<IEnumerable<Modality>> GetComboAsync() => await _modalitiesRepository.GetComboAsync();

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _modalitiesRepository.GetTotalPagesAsync(pagination);
    }
}
