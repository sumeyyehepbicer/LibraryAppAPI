using LibraryApp.Shared.DTOs.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAll(CancellationToken cancellationToken);
        Task<BookDto> GetById(int id, CancellationToken cancellationToken);
    }
}
