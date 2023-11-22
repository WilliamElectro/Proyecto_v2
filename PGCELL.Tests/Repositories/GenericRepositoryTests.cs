using Microsoft.EntityFrameworkCore;
using PGCELL.Backend.Data;
using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Tests.Shared;

namespace PGCELL.Tests.Repositories
{
    [TestClass]
    public class GenericRepositoryTests
    {
        private DataContext _context = null!;
        private DbContextOptions<DataContext> _options = null!;
 

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(_options);
     
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

    }
}