using Library.DAL.Model;
using Library.Domain.Response;
using Library.Domain.ViewModels.Client;
using Library.Domain.ViewModels.TransactBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
    public interface ITransactBookService
    {
        Task<IBaseResponse<TransactBook>> GiveBook(int id, int cid, int count);
        Task<IBaseResponse<bool>> ReturnBook(int id, int count);
        Task<IBaseResponse<IEnumerable<TransactBookViewModel>>> GetAllTransactBooksByID(int id);
    }
}
