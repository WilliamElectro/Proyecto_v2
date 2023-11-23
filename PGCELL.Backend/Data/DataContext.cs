using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PGCELL.Shared.Entites;

namespace PGCELL.Backend.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Modality> Modalities { get; set; }

        public DbSet<TypeNovelty> TypeNovelties { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<WorkSchedule> WorkSchedules { get; set; }

        public DbSet<Novelty> Novelties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(s => new { s.CountryId, s.Name }).IsUnique();
            modelBuilder.Entity<City>().HasIndex(c => new { c.StateId, c.Name }).IsUnique();
            modelBuilder.Entity<Modality>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<TypeNovelty>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Worker>().HasIndex(c => c.Document).IsUnique();
            modelBuilder.Entity<Contract>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Novelty>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<WorkSchedule>().HasIndex(c => c.Name).IsUnique();

            DisableCascadingDelete(modelBuilder);
        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}