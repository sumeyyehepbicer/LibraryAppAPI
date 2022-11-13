using LibraryApp.Application.Interfaces;
using LibraryApp.Domain.Settings;
using LibraryApp.Infrastructure.Exceptions;
using LibraryApp.Persistence.Contexts;
using LibraryApp.Shared.DTOs.UserDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly LibraryContext _context;
        private readonly JwtSettings _jwtSettings;
        public AccountService(LibraryContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            AuthenticateResponse authenticateResponse = new AuthenticateResponse();

            if (string.IsNullOrEmpty(request.Username))
                throw new AppException("Eposta boş olamaz.");

            if (string.IsNullOrEmpty(request.Password))
                throw new AppException("Parola boş olamaz.");

            var user = await _context.Users.FirstOrDefaultAsync(s => s.Username == request.Username, cancellationToken);

            if (user == null)
                throw new AppException("Kullanıcı bulunamadı.");

            if (user.Password != request.Password)
                throw new AppException("Parola hatalı.");

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.GivenName, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var expDate = DateTime.Now.AddYears(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expDate,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return new AuthenticateResponse(user, jwtToken);
        }
    }
}
