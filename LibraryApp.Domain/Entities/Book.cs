using LibraryApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Entities
{
    public class Book : AuditableEntity
    {        
        public string Name { get; set; }        
        public int SystemAmount { get; set; }
    }
}
