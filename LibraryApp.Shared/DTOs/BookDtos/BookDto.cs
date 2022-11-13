using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Shared.DTOs.BookDtos
{
    public class BookDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SystemAmount { get; set; }
        public int AmountOfOutside { get; set; }
        public int RemainingInTheLibrary
        {
            get
            {
                return SystemAmount - AmountOfOutside;
            }
        }

    }
}
