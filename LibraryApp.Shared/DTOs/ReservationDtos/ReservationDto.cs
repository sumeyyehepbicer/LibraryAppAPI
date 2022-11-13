using LibraryApp.Domain.Entities;
using LibraryApp.Shared.DTOs.BookDtos;
using LibraryApp.Shared.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Shared.DTOs.ReservationDtos
{
    public class ReservationDto:BaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool IsReturned { get; set; }
    }
    
}
