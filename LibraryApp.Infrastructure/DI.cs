using LibraryApp.Application.Interfaces;
using LibraryApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Infrastructure
{
    public static class DI
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IReservationService, ReservationService>();

        }
    }
}
