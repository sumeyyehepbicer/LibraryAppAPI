using LibraryApp.Application.Interfaces;
using LibraryApp.Shared.DTOs.ReservationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController( IReservationService reservationService)
        {            
            _reservationService = reservationService;
        }

        [HttpGet("get-me-reservations")]
        public async Task<List<ReservationDto>> GetMeReservations(CancellationToken cancellationToken)
        {
            return await _reservationService.GetMeReservations(cancellationToken);
        }

        [HttpGet("get-all")]
        public async Task<List<ReservationDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _reservationService.GetAll(cancellationToken);
        }

        [HttpGet("get-by-id")]
        public async Task<ReservationDto> GetById(int id,CancellationToken cancellationToken)
        {
            return await _reservationService.GetById(id,cancellationToken);
        }

        [HttpPost("create-reservation")]
        public async Task<ReservationDto> CreateReservation(CreateReservationDto createReservation,CancellationToken cancellationToken)
        {
            return await _reservationService.CreateReservation(createReservation,cancellationToken);
        }

        [HttpPost("create-returned")]
        public async Task<ReservationDto> CreateReturned(int reservationId, CancellationToken cancellationToken)
        {
            return await _reservationService.CreateReturned(reservationId, cancellationToken);
        }

    }
}
