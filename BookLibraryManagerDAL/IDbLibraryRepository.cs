using BookLibraryManagerDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryManagerDAL
{
    public interface IDbLibraryRepository
    {
        Task<Book> GetNearestBooks(string cityName, float latitude, float longitude);
        Task<IEnumerable<Library>> GetLibrariesByCity(string cityName);
    }
}