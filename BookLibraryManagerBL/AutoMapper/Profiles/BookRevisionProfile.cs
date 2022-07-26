using AutoMapper;
using BookLibraryManagerBL.DTOs;
using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerBL.AutoMapper.Profiles
{
    public class BookRevisionProfile : Profile
    {
        public BookRevisionProfile()
        {
            CreateMap<BookRevisionDto, BookRevision>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.PublishingYear, opt => opt.MapFrom(src => src.PublishingYear))
                .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(src => src.PagesCount))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<BookRevision, BookRevisionDto>()
                .ForMember(dest => dest.RevisionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.PublishingYear, opt => opt.MapFrom(src => src.PublishingYear))
                .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(src => src.PagesCount))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
