using AutoMapper;
using BookLibraryManagerBL.Models;
using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerBL.AutoMapper.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<LibraryDto, Library>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.FullAddress));

            CreateMap<Library, LibraryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.FullAddress));
        }
    }
}
