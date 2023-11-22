using Microsoft.EntityFrameworkCore;
using PGCELL.Backend.Data;
using PGCELL.Backend.Helpers;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Backend.Repositories
{
    public class WorkersRepository : GenericRepository<Worker>, IWorkersRepository
    {
        private readonly DataContext _context;

        public WorkersRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<Worker>> GetAsync(int id)
        {
            var worker = await _context.Workers
                .Include(w => w.Modality)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (worker == null)
            {
                return new Response<Worker>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            return new Response<Worker>
            {
                WasSuccess = true,
                Result = worker
            };
        }

        public override async Task<Response<IEnumerable<Worker>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Workers
                .Include(w => w.Modality)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Document.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new Response<IEnumerable<Worker>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Document)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<Worker>> GetComboAsync()
        {
            return await _context.Workers
                .OrderBy(c => c.Document)
                .ToListAsync();
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Workers
                .Include(w => w.Modality)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Document.ToLower().Contains(pagination.Filter.ToLower()));
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
