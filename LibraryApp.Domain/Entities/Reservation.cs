using LibraryApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Entities
{
    public class Reservation:AuditableEntity
    {
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool IsReturned { get; set; }=false;
    }
}
