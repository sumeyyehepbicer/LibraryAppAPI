using LibraryApp.Application.Interfaces;
using LibraryApp.Shared.DTOs.BookDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("get-all")]
        public async Task<List<BookDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _bookService.GetAll(cancellationToken);
        }

        [HttpGet("get-by-id")]
        public async Task<BookDto> GetById(int id, CancellationToken cancellationToken)
        {
            return await _bookService.GetById(id, cancellationToken);
        }
    }
}
