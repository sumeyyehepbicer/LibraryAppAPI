using AutoMapper;
using LibraryApp.Domain.Entities;
using LibraryApp.Shared.DTOs.BookDtos;

namespace LibraryApp.Presentation.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
