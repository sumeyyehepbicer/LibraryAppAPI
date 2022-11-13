using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibraryApp.Shared.DTOs
{
    public abstract class BaseDto
    {
      
        [JsonIgnore]
        public DateTime? DateCreated { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
}
