using AutoMapper;
using LibraryApp.Application.Interfaces;
using LibraryApp.Domain.Entities;
using LibraryApp.Infrastructure.Exceptions;
using LibraryApp.Persistence.Contexts;
using LibraryApp.Shared.DTOs.BookDtos;
using LibraryApp.Shared.DTOs.ReservationDtos;
using LibraryApp.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private readonly LibraryContext _context;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public readonly IMapper _mapper;
        public readonly IBookService _bookService;
        public ReservationService(LibraryContext context, IMapper mapper,
            IAuthenticatedUserService authenticatedUserService, IBookService bookService)
        {
            _context = context;
            _mapper = mapper;
            _authenticatedUserService = authenticatedUserService;
            _bookService = bookService;
        }

        public async Task<List<ReservationDto>> GetMeReservations(CancellationToken cancellationToken)
        {
            var reservations = await _context.Reservations
                .Include(op => op.Book)
                .Include(op => op.User)
                .Where(op => op.UserId == int.Parse(_authenticatedUserService.UserId)).ToListAsync();
            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
            return reservationDtos;
        }
        public async Task<List<ReservationDto>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                List<ReservationDto> reservationDtos = new List<ReservationDto>();
                var reservations = await _context.Reservations.ToListAsync(cancellationToken);
                if (reservations.Count == 0)
                    throw new AppException("Rezervasyon bulunamadı.");
                else
                    reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
                return reservationDtos;
            }
            catch (Exception ex)
            {

                throw new AppException(ex.Message);

            }
        }

        public async Task<ReservationDto> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var reservation = await _context.Reservations.FirstOrDefaultAsync(op => op.Id == id);
                if (reservation is null)
                    throw new AppException("Rezervasyon bulunamadı.");

                var reservationDto = _mapper.Map<ReservationDto>(reservation);
                return reservationDto;
            }
            catch (Exception ex)
            {

                throw new AppException(ex.Message);

            }
        }
        public async Task<ReservationDto> CreateReservation(CreateReservationDto createReservation, CancellationToken cancellationToken)
        {
            Reservation reservation = null;
            int maxBookCount = 3;
            reservation = _mapper.Map<Reservation>(createReservation);

            var book = await _bookService.GetById(createReservation.BookId, cancellationToken);
            if (book is null)
                throw new AppException($"{createReservation.BookId}'li kitap bulunamadı.");
            else
            {
                var reservationByBookIdCount = await _context.Reservations
                            .Where(op => op.BookId == createReservation.BookId && !op.IsReturned).CountAsync();
                if (reservationByBookIdCount == book.SystemAmount)
                    throw new AppException($"Sistemdeki tüm {book.Name} adlı kitaplar diğer kullanıcılardadır.Lütfen daha sonra tekrar deneyiniz.");
            }

            var reservationByUserId = await _context.Reservations
           .Where(op => op.UserId == int.Parse(_authenticatedUserService.UserId) && !op.IsReturned).ToListAsync();

            if (reservationByUserId.Count == maxBookCount)
                throw new AppException($"{maxBookCount}'ten fazla kitap alamazsınız.");

            reservation.UserId = int.Parse(_authenticatedUserService.UserId);
            reservation.ReturnedDate = createReservation.ReservationDate.AddDays(7);
            await _context.Reservations.AddAsync(reservation, cancellationToken);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> CreateReturned(int reservationId, CancellationToken cancellationToken)
        {
            var reservationById = await _context.Reservations
               .FirstOrDefaultAsync(op => op.Id == reservationId);

            if (reservationById is null)
                throw new AppException($"Böyle bir rezervasyon bulunamadı.");

            reservationById.IsReturned = true;
            _context.Reservations.Update(reservationById);
            await _context.SaveChangesAsync();

            return await this.GetById(reservationId, cancellationToken);
        }

        public async Task<List<ReservationDto>> GetByBookId(int bookId, CancellationToken cancellationToken)
        {
            try
            {
                List<ReservationDto> reservationDtos = new List<ReservationDto>();
                var reservations = await _context.Reservations.Where(op => op.BookId == bookId).ToListAsync(cancellationToken);
                if (reservations.Count == 0)
                    throw new AppException($"{bookId}'li kitaba ait rezervasyon bulunamadı.");
                else
                    reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
                return reservationDtos;
            }
            catch (Exception ex)
            {

                throw new AppException(ex.Message);

            }
        }
    }
}
