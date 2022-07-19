using BookLibraryManagerDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryManagerDAL
{
    public class EFCoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Point> Locations { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Library> Libraries { get; set; }

        public DbSet<BookRevision> BookRevisions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<LibraryBook> LibraryBooks { get; set; }

        public DbSet<RentBook> RentBooks { get; set; }



        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
