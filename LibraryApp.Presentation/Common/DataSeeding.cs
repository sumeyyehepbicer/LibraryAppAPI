using LibraryApp.Domain.Entities;
using LibraryApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Presentation.Common
{
    public static class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<LibraryContext>();
            context.Database.Migrate();
            if (context.Users.Count() == 0)
            {
                context.Users.AddRange(new List<User>()
                {
                    new User{ Id = 1,FirstName="Test",LastName="Test",Username="test",EmailAddress="test@gmail.com",Password="111111",PhoneNumber=5333333333,DateCreated=DateTime.Now,CreatedBy="System",LastModifiedDate=DateTime.Now,LastModifiedBy="System",IsDeleted=false},
                    new User{ Id = 2,FirstName="user",LastName="user",Username="user",EmailAddress="user@gmail.com",Password="111111",PhoneNumber=5555555555,DateCreated=DateTime.Now,CreatedBy="System",LastModifiedDate=DateTime.Now,LastModifiedBy="System",IsDeleted=false}
                });
            }

            if (context.Books.Count() == 0)
            {
                context.Books.AddRange(new List<Book>()
                {
                    new Book{ Id = 1,Name="What is Lorem Ipsum?",SystemAmount=2,DateCreated=DateTime.Now,CreatedBy="System",LastModifiedDate=DateTime.Now,LastModifiedBy="System",IsDeleted=false},
                    new Book{ Id = 2,Name="Why do we use it?",SystemAmount=5,DateCreated=DateTime.Now,CreatedBy="System",LastModifiedDate=DateTime.Now,LastModifiedBy="System",IsDeleted=false},
                    new Book{ Id = 3,Name="Where does it come from?",SystemAmount=6,DateCreated=DateTime.Now,CreatedBy="System",LastModifiedDate=DateTime.Now,LastModifiedBy="System",IsDeleted=false},
                    new Book{ Id = 4,Name="Where can I get some?",SystemAmount=4,DateCreated=DateTime.Now,CreatedBy="System",LastModifiedDate=DateTime.Now,LastModifiedBy="System",IsDeleted=false},
                    new Book{ Id = 5,Name="Lorem Ipsum",SystemAmount=2,DateCreated=DateTime.Now,CreatedBy="System",LastModifiedDate=DateTime.Now,LastModifiedBy="System",IsDeleted=false},
                });
            }

            if (context.Reservations.Count() == 0)
            {
                context.Reservations.AddRange(new List<Reservation>()
                {
                    new Reservation{ Id = 1,UserId=1,BookId=1,ReservationDate=DateTime.Now,ReturnedDate=DateTime.Now.AddDays(7),IsReturned=false,DateCreated=DateTime.Now,CreatedBy="System",LastModifiedDate=DateTime.Now,LastModifiedBy="System",IsDeleted=false},
                    new Reservation{ Id = 2,UserId=1,BookId=2,ReservationDate=DateTime.Now,ReturnedDate=DateTime.Now.AddDays(7),IsReturned=true,DateCreated=DateTime.Now,CreatedBy="System",LastModifiedDate=DateTime.Now,LastModifiedBy="System",IsDeleted=false},
                });
            }
            context.SaveChanges();
        }
    }
}
