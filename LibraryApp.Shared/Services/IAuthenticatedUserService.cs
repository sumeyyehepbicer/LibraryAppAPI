using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Shared.Services
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string Username { get; }
    }
}
