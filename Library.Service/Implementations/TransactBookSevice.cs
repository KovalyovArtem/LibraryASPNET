using Library.DAL.Implementations;
using Library.DAL.Interfaces;
using Library.DAL.Model;
using Library.Domain.Enum;
using Library.Domain.Response;
using Library.Domain.ViewModels.Book;
using Library.Domain.ViewModels.Client;
using Library.Domain.ViewModels.TransactBook;
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
    public class TransactBookSevice : ITransactBookService
    {
        private readonly ITransactBookDAL _transactBookDAL;
        private ILogger<BookService> _logger;
        public TransactBookSevice(ITransactBookDAL transactBookDAL, ILogger<BookService> logger)
        {
            _transactBookDAL = transactBookDAL;
            _logger = logger;
        }

        public async Task<IBaseResponse<IEnumerable<TransactBookViewModel>>> GetAllTransactBooksByID(int id)
        {
            try
            {
                var books = await _transactBookDAL.GetAllTransactBooks(id);

                return new BaseResponse<IEnumerable<TransactBookViewModel>>()
                {
                    Data = books,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TransactBookSevice.GetAllTransactBooksByID]: {ex.Message}");
                return new BaseResponse<IEnumerable<TransactBookViewModel>>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<TransactBook>> GiveBook(int id, int cid, int count)
        {
            try
            {
                _logger.LogInformation($"Запрос на выдачу книг клиенту - {cid}");

                if (count < 1)
                {
                    return new BaseResponse<TransactBook>()
                    {
                        Description = "Вы не можете взять менее одной книги",
                        StatusCode = StatusCode.QuantityError
                    };
                }

                var exist = await _transactBookDAL.ExistBook(id, cid);
                if (exist)
                {
                    return new BaseResponse<TransactBook>()
                    {
                        Description = "У вас уже есть эта книга",
                        StatusCode = StatusCode.TransactionBookHasAlready
                    };
                }

                var bookAccess = await _transactBookDAL.QuantityExist(id, count);
                if (!bookAccess)
                {
                    return new BaseResponse<TransactBook>()
                    {
                        Description = "Количество, которое вы хотите взять больше чем у нас есть",
                        StatusCode = StatusCode.QuantityMoreThenHave
                    };
                }

                TransactBook book = new TransactBook()
                {
                    BookID = id,
                    ClientID = cid,
                    Quantity = count,
                    RecieveDate = DateTime.Today
                };

                await _transactBookDAL.GiveBook(book);

                _logger.LogInformation($"Книга была выдана - {book.BookID} {book.RecieveDate}");
                return new BaseResponse<TransactBook>()
                {
                    Description = "Книга была успешно выдана",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ClientService.GiveBook]: {ex.Message}");
                return new BaseResponse<TransactBook>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> ReturnBook(int id, int count)
        {
            try
            {
                _logger.LogInformation($"Запрос на возврат книги - { id }");

                if(count < 1)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Вы не можете взять менее вернуть книги",
                        StatusCode = StatusCode.QuantityError
                    };
                }

                var transactBook = await _transactBookDAL.QuantityReturnExist(id, count);
                if (transactBook)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Вы не можете отдать книг больше, чем у вас есть",
                        StatusCode = StatusCode.QuantityMoreThenHave
                    };
                }

                await _transactBookDAL.ReturnBook(id, count);

                _logger.LogInformation($"Книга была возвращена - {id}");
                return new BaseResponse<bool>()
                {
                    Description = "Книга была успешно возвращена",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ClientService.ReturnBook]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
