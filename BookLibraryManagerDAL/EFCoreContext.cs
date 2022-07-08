using BookLibraryManagerDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryManagerDAL
{
    public class EFCoreContext : DbContext
    {
        public DbSet<BookDTO> Books { get; set; }

        public DbSet<ClientDTO> Clients { get; set; }

        public DbSet<LibraryDTO> Libraries { get; set; }

        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
