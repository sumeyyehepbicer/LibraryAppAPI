using LibraryApp.Shared.DTOs.ReservationDtos;

namespace LibraryApp.Application.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAll(CancellationToken cancellationToken);
        Task<ReservationDto> GetById(int id,CancellationToken cancellationToken);
        Task<List<ReservationDto>> GetMeReservations(CancellationToken cancellationToken);
        Task<List<ReservationDto>> GetByBookId(int bookId,CancellationToken cancellationToken);
        Task<ReservationDto> CreateReservation(CreateReservationDto createReservation,CancellationToken cancellationToken);
        Task<ReservationDto> CreateReturned(int reservationId,CancellationToken cancellationToken);
    }
}
