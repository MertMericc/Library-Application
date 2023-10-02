using Library.BLL.Abstract;
using Library.BLL.Constants;
using Library.Core.Result;
using Library.Dal.Abstract;
using Library.Entity.Concrete;
using Library.Entity.Dto.BookDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Concrete
{
    public class LibraryManager : ILibraryService
    {
        private readonly IBookDal _bookDal;
        private readonly ILoanDal _loanDal;
        private readonly ILogger<LibraryManager> _logger;

        public LibraryManager(IBookDal bookDal, ILoanDal loanDal, ILogger<LibraryManager> logger)
        {
            _bookDal = bookDal;
            _loanDal = loanDal;
            _logger = logger;
        }

        public IDataResult<bool> AddBook(BookAddDto bookAddDto)
        {
            try
            {
                Book bookAdd = new()
                {
                    Name = bookAddDto.Name,
                    Author = bookAddDto.Author,
                    ImageUrl = bookAddDto.ImageUrl,
                    IsAvailableInLibrary = bookAddDto.IsAvailableInLibrary,
                    Status = true
                };
                _bookDal.Add(bookAdd);
                 _logger.LogInformation("Kitap eklendi. Kitap adı: {KitapAdi}");
                return new SuccessDataResult<bool>(true);
               
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<bool>(false);
            }
        }

        public IDataResult<bool> BorrowBook(int bookId, DateTime dueDate, string userName)
        {
            try
            {
                if (bookId == null)
                {
                    return new ErrorDataResult<bool>(false, "bookId cannot be null", Messages.err_null);
                }

                if (dueDate == DateTime.MinValue || dueDate < DateTime.Now)
                {
                    return new ErrorDataResult<bool>(false, "Invalid dueDate", Messages.err_null);
                }

                if (string.IsNullOrWhiteSpace(userName))
                {
                    return new ErrorDataResult<bool>(false, "userName cannot be empty", Messages.err_null);
                }

                var getBook = _bookDal.Get(x => x.BookId == bookId);
                if (getBook == null)
                {
                    return new ErrorDataResult<bool>(false, "book not found", Messages.book_not_found);
                }

                var loan = new Loan
                {
                    BookId = getBook.BookId,
                    LoanDate = DateTime.Now,
                    DueDate = dueDate,
                    IsReturned = false,
                    UserName = userName
                };
                _loanDal.Add(loan);
                getBook.IsAvailableInLibrary = false;
                _bookDal.Update(getBook);
                return new SuccessDataResult<bool>(true, "Book borrowed successfully", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.err_null);
            }
        }



        public IDataResult<List<BookGetAllResponseDto>> GetAllBook()
        {
            try
            {
                var bookDatas = _bookDal.GetList().OrderBy(x => x.Name).ToList();
                if (bookDatas.Count <= 0)
                {
                    return new ErrorDataResult<List<BookGetAllResponseDto>>(null, "books not found", Messages.book_not_found);
                }
                var list = new List<BookGetAllResponseDto>();
                foreach (var book in bookDatas)
                {
                    var bookResponseDto = new BookGetAllResponseDto
                    {
                        BookId = book.BookId,
                        Name = book.Name,
                        Status = book.Status,
                        Author = book.Author,
                        ImageUrl = book.ImageUrl,
                        IsAvailableInLibrary = book.IsAvailableInLibrary
                    };

                    if (!book.IsAvailableInLibrary)
                    {
                        var loan = _loanDal.Get(x => x.BookId == book.BookId && !x.IsReturned);
                        if (loan != null)
                        {
                            bookResponseDto.UserName = loan.UserName;
                        }
                    }

                    list.Add(bookResponseDto);
                }
                return new SuccessDataResult<List<BookGetAllResponseDto>>(list, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<BookGetAllResponseDto>>(null, ex.Message, Messages.err_null);
            }
        }


        public IDataResult<List<BookResponseDto>> GetAllBooksInLibrary()
        {
            try
            {
                var inLibraryBooks = _bookDal.GetList(x => x.IsAvailableInLibrary == true).OrderBy(x => x.Name).ToList();
                if (inLibraryBooks.Count <= 0)
                {
                    return new ErrorDataResult<List<BookResponseDto>>(null, "in library book not found", Messages.book_not_found);
                }
                var list = new List<BookResponseDto>();
                foreach (var book in inLibraryBooks)
                {
                    list.Add(new BookResponseDto
                    {
                        BookId = book.BookId,
                        Name = book.Name,
                        Status = book.Status,
                        Author = book.Author,
                        ImageUrl = book.ImageUrl,
                        IsAvailableInLibrary = book.IsAvailableInLibrary
                    });
                }
                return new SuccessDataResult<List<BookResponseDto>>(list, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<BookResponseDto>>(null, ex.Message, Messages.err_null);
            }
        }



        public IDataResult<BookResponseDto> GetBookById(int bookId)
        {
            try
            {
                var getBookById = _bookDal.Get(x => x.BookId == bookId);
                if (getBookById == null)
                {
                    return new ErrorDataResult<BookResponseDto>(null, "book not found", Messages.book_not_found);
                }
                var bookResponseDto = new BookResponseDto
                {
                    BookId = bookId,
                    Name = getBookById.Name,
                    Status = getBookById.Status,
                    Author = getBookById.Author,
                    ImageUrl = getBookById.ImageUrl,
                    IsAvailableInLibrary = getBookById.IsAvailableInLibrary
                };
                return new SuccessDataResult<BookResponseDto>(bookResponseDto, "Ok", Messages.success);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<BookResponseDto>(null, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<List<BookResponseDto>> GetBooksByAuthor(string authorName)
        {
            try
            {
                var booksByAuthor = _bookDal.GetList(x => x.Author.Trim().ToLower().Contains(authorName.Trim().ToLower()));

                if (booksByAuthor == null || !booksByAuthor.Any())
                {
                    return new ErrorDataResult<List<BookResponseDto>>(null, "No books found for the author", Messages.book_not_found);
                }

                var bookResponseDtos = booksByAuthor.Select(book => new BookResponseDto
                {
                    BookId = book.BookId,
                    Name = book.Name,
                    Author = book.Author,
                    Status = book.Status,
                    ImageUrl = book.ImageUrl,
                    IsAvailableInLibrary = book.IsAvailableInLibrary
                }).ToList();

                return new SuccessDataResult<List<BookResponseDto>>(bookResponseDtos, "Books found for the author", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<BookResponseDto>>(null, ex.Message, Messages.err_null);
            }
        }



        public IDataResult<bool> ReturnBook(int bookId)
        {
            try
            {
                var loan = _loanDal.Get(l => l.BookId == bookId && !l.IsReturned);

                if (loan != null)
                {
                    loan.IsReturned = true;
                    loan.DueDate = DateTime.Now;
                    _loanDal.Update(loan);

                    var book = _bookDal.Get(b => b.BookId == loan.BookId);
                    if (book != null)
                    {
                        book.IsAvailableInLibrary = true;
                        _bookDal.Update(book);
                    }

                    return new SuccessDataResult<bool>(true);
                }

                return new ErrorDataResult<bool>(false);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<bool>(false, ex.Message, Messages.err_null);
            }


        }

        public IDataResult<bool> UpdateBook(BookUpdateDto bookUpdateDto)
        {
            try
            {
                var book = _bookDal.Get(x => x.BookId == bookUpdateDto.BookId);

                if (book == null)
                {
                    return new ErrorDataResult<bool>(false, "Book not found", Messages.book_not_found);
                }

                if (!string.IsNullOrEmpty(bookUpdateDto.Name))
                {
                    book.Name = bookUpdateDto.Name;
                }

                if (!string.IsNullOrEmpty(bookUpdateDto.Author))
                {
                    book.Author = bookUpdateDto.Author;
                }

                if (!string.IsNullOrEmpty(bookUpdateDto.ImagteUrl))
                {
                    book.ImageUrl = bookUpdateDto.ImagteUrl;
                }

                if (bookUpdateDto.IsAvailableInLibrary != null)
                {
                    book.IsAvailableInLibrary = bookUpdateDto.IsAvailableInLibrary.Value;
                }

                _bookDal.Update(book);

                return new SuccessDataResult<bool>(true, "Book updated successfully", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.err_null);
            }
        }


        public IDataResult<bool> UpdateStatusBook(int bookId)
        {
            try
            {
                var getBook = _bookDal.Get(x => x.BookId == bookId);
                if (getBook == null)
                {
                    return new ErrorDataResult<bool>(false, "book not found", Messages.err_null);
                }
                getBook.Status = !getBook.Status;
                _bookDal.Update(getBook);
                return new SuccessDataResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<bool>(false,ex.Message, Messages.err_null);
            }
        }
    }
}
