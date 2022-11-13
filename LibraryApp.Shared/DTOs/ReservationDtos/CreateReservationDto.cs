using LibraryApp.Shared.DTOs.BookDtos;
using LibraryApp.Shared.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Shared.DTOs.ReservationDtos
{
    public class CreateReservationDto:BaseDto
    {
        public int BookId { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
