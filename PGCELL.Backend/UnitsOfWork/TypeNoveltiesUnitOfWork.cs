using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PGCELL.Backend.UnitsOfWork
{
    public class TypeNoveltiesUnitOfWork : GenericUnitOfWork<TypeNovelty>, ITypeNoveltiesUnitOfWork
    {
        private readonly ITypeNoveltiesRepository _typeNoveltiesRepository;

        public TypeNoveltiesUnitOfWork(IGenericRepository<TypeNovelty> repository, ITypeNoveltiesRepository typeNoveltiesRepository) : base(repository)
        {
            _typeNoveltiesRepository = typeNoveltiesRepository;
        }

        public override async Task<Response<IEnumerable<TypeNovelty>>> GetAsync(PaginationDTO pagination) => await _typeNoveltiesRepository.GetAsync(pagination);

        public async Task<IEnumerable<TypeNovelty>> GetComboAsync() => await _typeNoveltiesRepository.GetComboAsync();

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _typeNoveltiesRepository.GetTotalPagesAsync(pagination);
    }
}
