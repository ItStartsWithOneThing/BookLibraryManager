using BookLibraryManagerBL.DTOs;
using BookLibraryManagerBL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryManagerBL.Services.LibrariesService
{
    public interface ILibrariesService
    {
        Task<Guid> CreateLibrary(LibraryDto library);
        Task<IEnumerable<LibraryDto>> GetNearestLibraries(string cityName, double latitude, double longitude, int librariesAmount);
    }
}