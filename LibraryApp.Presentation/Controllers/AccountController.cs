using LibraryApp.Application.Interfaces;
using LibraryApp.Shared.DTOs.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Authenticate")]
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            return await _accountService.Authenticate(request, cancellationToken);
        }
    }
}
