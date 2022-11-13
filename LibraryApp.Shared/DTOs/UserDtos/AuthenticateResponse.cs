using LibraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Shared.DTOs.UserDtos
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse()
        {

        }
        public User User { get; set; }
        public string accessToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken)
        {
            User = user;
            accessToken = jwtToken;
        }
    }
}
