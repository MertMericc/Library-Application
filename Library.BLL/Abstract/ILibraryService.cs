using Library.Core.Result;
using Library.Entity.Dto.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Abstract
{
    public interface ILibraryService
    {
        IDataResult<bool> AddBook(BookAddDto bookAddDto);
        IDataResult<bool> UpdateBook(BookUpdateDto bookUpdateDto);
        IDataResult<bool> UpdateStatusBook(int bookId);
        IDataResult<BookResponseDto> GetBookById(int bookId);
        IDataResult<List<BookGetAllResponseDto>> GetAllBook();
        IDataResult<List<BookResponseDto>> GetBooksByAuthor(string authorName);
        IDataResult<bool> BorrowBook(int bookId, DateTime dueDate,string userName);
        IDataResult<bool> ReturnBook(int loanId);
        IDataResult<List<BookResponseDto>> GetAllBooksInLibrary();
    }
}
