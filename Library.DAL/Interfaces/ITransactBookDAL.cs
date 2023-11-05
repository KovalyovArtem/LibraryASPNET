using Library.DAL.Model;
using Library.Domain.ViewModels.TransactBook;

namespace Library.DAL.Interfaces
{
    public interface ITransactBookDAL
    {
        Task GiveBook(TransactBook book);
        Task ReturnBook(int id, int quantity);
        Task<bool> ExistBook(int bookId, int clientId);
        Task<bool> QuantityExist(int id, int count);
        Task<bool> QuantityReturnExist(int id, int count);
        Task<IEnumerable<TransactBookViewModel>> GetAllTransactBooks(int id);
    }
}
