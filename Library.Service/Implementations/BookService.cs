using Library.DAL.Interfaces;
using Library.DAL.Model;
using Library.Domain.Enum;
using Library.Domain.Response;
using Library.Domain.ViewModels.Book;
using Library.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookDAL _bookDAL;
        private ILogger<BookService> _logger;

        public BookService(IBookDAL bookDAL, ILogger<BookService> logger)
        {
            _bookDAL = bookDAL;
            _logger = logger;
        }

        public async Task<IBaseResponse<IEnumerable<string>>> GetTitleBooks()
        {
            try
            {
                var titles = await _bookDAL.GetTitleOfBooks();

                var count = titles.Count();

                return new BaseResponse<IEnumerable<string>>()
                {
                    Data = titles,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BookService.GetTitleBooks]: {ex.Message}");
                return new BaseResponse<IEnumerable<string>>()
                {
                    Data = null,
                    StatusCode = StatusCode.OK
                };
            }
        }

        public async Task<IBaseResponse<Book>> Create(CreateBookViewModel model)
        {
            try
            {
                model.Validate();

                _logger.LogInformation($"Запрос на создание книги - {model.Title}");

                var book = await _bookDAL.GetBookByTitle(model.Title);

                if (book != null)
                {
                    return new BaseResponse<Book>()
                    {
                        Description = "Такая книга уже есть",
                        StatusCode = StatusCode.BookHasAlready
                    };
                }

                book = new Book()
                {
                    Title = model.Title,
                    Author = model.Author,
                    Quantity = model.Quantity,
                    Discription = model.Discription
                };

                await _bookDAL.Create(book);

                _logger.LogInformation($"Книга создалась - {model.Title} {model.Author}");
                return new BaseResponse<Book>()
                {
                    Description = "Книга успешно создалась",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BookService.Create]: {ex.Message}");
                return new BaseResponse<Book>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Book>> UpdateDiscription(string Title, string Discription)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Title))
                    throw new ArgumentNullException(Title, "Укажите название книги");
                if (string.IsNullOrWhiteSpace(Discription))
                    throw new ArgumentNullException(Discription, "Укажите описание книги");

                var books = await _bookDAL.GetBookByTitleI(Title);
                if(books.Count() == 0)
                {
                    return new BaseResponse<Book>()
                    {
                        Description = "Книги не существует",
                        StatusCode = StatusCode.BookHasNotExist
                    };
                }

                _logger.LogInformation($"Запрос на изменение описания книги - {Title}");
                await _bookDAL.UpdateDiscripton(Title, Discription);

                _logger.LogInformation($"Описание книги изменилось - {Title} {Discription}");
                return new BaseResponse<Book>()
                {
                    Description = "Описание книги успешно изменено",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BookService.Update]: {ex.Message}");
                return new BaseResponse<Book>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Book>> UpdateDiscription(int id, string Discription)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Discription))
                    throw new ArgumentNullException(Discription, "Укажите описание книги");

                var books = await _bookDAL.GetBookByID(id);
                if (books == null)
                {
                    return new BaseResponse<Book>()
                    {
                        Description = "Книги не существует",
                        StatusCode = StatusCode.BookHasNotExist
                    };
                }

                _logger.LogInformation($"Запрос на изменение описания книги - {books.Title}");
                await _bookDAL.UpdateDiscripton(id, Discription);

                _logger.LogInformation($"Описание книги изменилось - {books.Title} {Discription}");
                return new BaseResponse<Book>()
                {
                    Description = "Описание книги успешно изменено",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BookService.Update]: {ex.Message}");
                return new BaseResponse<Book>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<BookViewModel>>> GetBooks()
        {
            try
            {
                var books = await _bookDAL.GetAllBooks();

                return new BaseResponse<IEnumerable<BookViewModel>>()
                {
                    Data = books,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[BookService.GetBooks]: {ex.Message}");
                return new BaseResponse<IEnumerable<BookViewModel>>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
