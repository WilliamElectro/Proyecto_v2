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
                .Include(w => w.WorkSchedule)
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
                .Include(w => w.WorkSchedule)
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
                .Include(w => w.WorkSchedule)
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

        public async Task<IEnumerable<Worker>> GetActiveWorkersAsync()
        {
            // Obtén la hora local
            DateTime localTime = DateTime.Now;
            TimeSpan currentTime = localTime.TimeOfDay;

            // Define los segmentos horarios
            TimeSpan morningStart = new TimeSpan(6, 0, 0); // 6:00 AM
            TimeSpan morningEnd = new TimeSpan(21, 0, 0); // 9:00 PM
            TimeSpan nightStart = new TimeSpan(21, 0, 1); // 9:00 PM + 1 segundo
            TimeSpan nightEnd = new TimeSpan(5, 59, 59); // 5:59:59 AM del día siguiente

            var workers = await _context.Workers
                .Include(w => w.WorkSchedule)
                .OrderBy(c => c.Document)
                .ToListAsync();

            var activeWorkers = workers.Where(worker =>
            {   //TODO: añadir validaciones
                if (worker.WorkSchedule == null)
                {
                    return false; // Si es nulo, no incluir al trabajador
                }

                var workScheduleName = worker.WorkSchedule.Name.ToLower();

                if (currentTime >= morningStart && currentTime <= morningEnd)
                {
                    // Es horario de día (6:00 AM - 9:00 PM)
                    // Implementa lógica específica para este segmento si es necesario
                    return workScheduleName.Contains("diurno");
                }
                else if ((currentTime >= nightStart && currentTime <= TimeSpan.FromHours(24)) || (currentTime >= TimeSpan.Zero && currentTime <= nightEnd))
                {
                    // Es horario de noche (9:00 PM - 6:00 AM)
                    // Implementa lógica específica para este segmento si es necesario
                    return workScheduleName.Contains("nocturno");
                }
                return false;
            });

            return activeWorkers;
        }
    }
}