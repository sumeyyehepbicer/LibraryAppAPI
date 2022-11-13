using LibraryApp.Domain.Common;
using LibraryApp.Domain.Entities;
using LibraryApp.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Persistence.Contexts
{
    public class LibraryContext : DbContext
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IDateTimeService _dateTimeService;
        public LibraryContext(DbContextOptions<LibraryContext> options,
            IAuthenticatedUserService authenticatedUser,
            IDateTimeService dateTimeService) : base(options)
        {
            _authenticatedUser = authenticatedUser;
            _dateTimeService = dateTimeService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<AuditableEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                string username = "SYSTEM";
                if (_authenticatedUser.UserId != null)
                    username = _authenticatedUser.Username;

                entry.Entity.LastModifiedDate = _dateTimeService.GetTurkeyToday();
                entry.Entity.LastModifiedBy = username;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = _dateTimeService.GetTurkeyToday();
                    entry.Entity.IsDeleted = false;
                    entry.Entity.CreatedBy = username;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Book>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Reservation>().HasQueryFilter(p => !p.IsDeleted);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
