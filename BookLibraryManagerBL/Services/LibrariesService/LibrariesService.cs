using AutoMapper;
using BookLibraryManagerBL.DTOs;
using BookLibraryManagerBL.Models;
using BookLibraryManagerDAL;
using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryManagerBL.Services.LibrariesService
{
    public class LibrariesService : ILibrariesService
    {
        private IDbLibraryRepository _libraryRepository;

        private IDbGenericRepository<Library> _genericLibraryRepository;

        private IMapper _mapper;

        private const double earthRadiusInMeters = 6371000;


        public LibrariesService(IDbLibraryRepository cityRepository, IDbGenericRepository<Library> genericLibraryRepository, IMapper mapper)
        {
            _libraryRepository = cityRepository;

            _genericLibraryRepository = genericLibraryRepository;

            _mapper = mapper;
        }

        public async Task<Guid> CreateLibrary(LibraryDto library)
        {
            var result = Guid.Empty;

            if (library != null)
            {
                var dbLibrary = _mapper.Map<Library>(library);

                result = await _genericLibraryRepository.Create(dbLibrary);
            }

            if (result != Guid.Empty)
            {
                return result;
            }

            return Guid.Empty;
        }

        public async Task<IEnumerable<LibraryDto>> GetNearestLibraries(string cityName, double latitude, double longitude, int librariesAmount)
        {
            LocationDto userLocation = new LocationDto() { Latitude = latitude, Longitude = longitude };

            var libraries = await _libraryRepository.GetLibrariesByCity(cityName);

            var nearestLibraries = SortLibraries(libraries, userLocation, librariesAmount);

            var result = _mapper.Map<IEnumerable<LibraryDto>>(nearestLibraries);

            return result;
        }

        private IEnumerable<Library> SortLibraries(IEnumerable<Library> libraries, LocationDto userLocation, int librariesAmount)
        {
            return libraries?.OrderBy(x => CalculateDistance(userLocation, x)).Take(librariesAmount);
        }

        private double CalculateDistance(LocationDto userLocation, Library library)
        {
            var dLat = DegToRad(library.Location.Latitude - userLocation.Latitude);
            var dLong = DegToRad(library.Location.Longitude - userLocation.Longitude);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegToRad(userLocation.Latitude)) * Math.Cos(DegToRad(library.Location.Latitude)) *
                    Math.Sin(dLong / 2) * Math.Sin(dLong / 2);

            var distance = earthRadiusInMeters * (2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)));

            return Math.Round(distance);
        }

        private double DegToRad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
