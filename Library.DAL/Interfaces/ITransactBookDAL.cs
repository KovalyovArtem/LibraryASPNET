using Library.Domain.ViewModels.Book;
using Library.Domain.ViewModels.TransactBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Interfaces
{
    public interface ITransactBookDAL
    {
        Task GiveBook<TransactBook>(TransactBook book);
        Task ReturnBook(int id, int quantity);
        Task<bool> ExistBook(int bookId, int clientId);
        Task<bool> QuantityExist(int id, int count);
        Task<bool> QuantityReturnExist(int id, int count);
        Task<IEnumerable<TransactBookViewModel>> GetAllTransactBooks(int id);
    }
}
