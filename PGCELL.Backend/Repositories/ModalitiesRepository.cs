using Microsoft.EntityFrameworkCore;
using PGCELL.Backend.Data;
using PGCELL.Backend.Helpers;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.Repositories
{
    public class ModalitiesRepository : GenericRepository<Modality>, IModalitiesRepository
    {
        private readonly DataContext _context;

        public ModalitiesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<Modality>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Modalities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new Response<IEnumerable<Modality>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<Modality>> GetComboAsync()
        {
            return await _context.Modalities
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Modalities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new Response<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }
    }
}
