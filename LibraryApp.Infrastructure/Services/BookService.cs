using AutoMapper;
using LibraryApp.Application.Interfaces;
using LibraryApp.Infrastructure.Exceptions;
using LibraryApp.Persistence.Contexts;
using LibraryApp.Shared.DTOs.BookDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;
        public readonly IMapper _mapper;
        public BookService(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BookDto>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                List<BookDto> bookDtos = new List<BookDto>();
                var books = await _context.Books.ToListAsync(cancellationToken);
                if (books.Count == 0)
                    throw new AppException("Kitap bulunamadı.");
                else
                {
                    foreach (var book in books)
                    {
                        var outSideBookCount = await _context.Reservations.Where(op => op.BookId == book.Id && !op.IsReturned).CountAsync();

                        var bookDto = _mapper.Map<BookDto>(book);
                        bookDto.AmountOfOutside = outSideBookCount;
                        bookDtos.Add(bookDto);
                    }
                    return bookDtos;
                }
                
            }
            catch (Exception ex)
            {

                throw new AppException(ex.Message);

            }

        }

        public async Task<BookDto> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(op => op.Id == id);
                if (book is null)
                    throw new AppException("Kitap bulunamadı.");

                var outSideBookCount = await _context.Reservations.Where(op => op.BookId == id && !op.IsReturned).CountAsync();

                var bookDto = _mapper.Map<BookDto>(book);
                bookDto.AmountOfOutside = outSideBookCount;
                return bookDto;
            }
            catch (Exception ex)
            {

                throw new AppException(ex.Message);

            }

        }
    }
}
