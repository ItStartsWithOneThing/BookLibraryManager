using BookLibraryManagerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryManagerDAL
{
    public class DbLibraryRepository : IDbLibraryRepository
    {
        private EFCoreContext _dbContext { get; set; }
        private DbSet<City> _dbSet;
        private DbSet<Library> _dbLibraries;

        public DbLibraryRepository(EFCoreContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<City>();
            _dbLibraries = _dbContext.Set<Library>();
        }

        public async Task<Book> GetNearestBooks(string cityName, float latitude, float longitude)
        {
            //var book = await _dbContext.Cities.Where(x => x.Name == cityName).Include(x => x.Libraries.Where(x => x.)).ToListAsync();

            return null;
        }

        public async Task<IEnumerable<Library>> GetLibrariesByCity(string cityName)
        {
            return await _dbLibraries.Include(x => x.City).Include(x => x.Location).Where(x => x.City.Name == cityName).ToListAsync();
        }
    }
}
