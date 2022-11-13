using AutoMapper;
using LibraryApp.Domain.Entities;
using LibraryApp.Shared.DTOs.ReservationDtos;

namespace LibraryApp.Presentation.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
        }
    }
}
