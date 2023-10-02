using Library.BLL.Abstract;
using Library.Entity.Dto.BookDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }
        [HttpPost("addbook")]
        public IActionResult AddBook(BookAddDto bookAddDto)
        {
            var result = _libraryService.AddBook(bookAddDto);
            return Ok(result);
        }

        [HttpPost("updatebook")]
        public IActionResult UpdateBook(BookUpdateDto bookUpdateDto)
        {
            var result = _libraryService.UpdateBook(bookUpdateDto);
            return Ok(result);
        }

        [HttpPost("updatestatusbook")]
        public IActionResult UpdateStatusBook(int bookId)
        {
            var result = _libraryService.UpdateStatusBook(bookId);
            return Ok(result);
        }

        [HttpGet("getbookbyid")]
        public IActionResult GetBookById(int bookId)
        {
            var result = _libraryService.GetBookById(bookId);
            return Ok(result);
        }

        [HttpGet("getallbook")]
        public IActionResult GetAllBook()
        {
            var result = _libraryService.GetAllBook();
            return Ok(result);
        }

        [HttpPost("getbookbyauthorname")]
        public IActionResult GetBookByAuthorname(string authorName)
        {
            var result = _libraryService.GetBooksByAuthor(authorName);
            return Ok(result);
        }

        [HttpPost("borrowbook")]
        public IActionResult BorrowBook(int bookId,DateTime dueDate,string username)
        {
            var result = _libraryService.BorrowBook(bookId,dueDate, username);
            return Ok(result);
        }

        [HttpPost("returnbook")]
        public IActionResult Return(int bookId)
        {
            var result = _libraryService.ReturnBook(bookId);
            return Ok(result);
        }


        [HttpGet("getallbookinlibrary")]
        public IActionResult GetAllBookInLibrary()
        {
            var result = _libraryService.GetAllBooksInLibrary();
            return Ok(result);
        }

    }
}
