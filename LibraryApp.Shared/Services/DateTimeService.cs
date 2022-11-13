using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetTurkeyToday()
        {
            return DateTime.UtcNow.AddHours(3);
        }
    }
}
