using Library.DAL.Model;
using Library.Domain.Response;
using Library.Domain.ViewModels.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
    public interface IBookService
    {
        Task<IBaseResponse<Book>> Create(CreateBookViewModel model);
        Task<IBaseResponse<Book>> UpdateDiscription(string Title, string Discription);
        Task<IBaseResponse<Book>> UpdateDiscription(int id, string Discription);
        Task<IBaseResponse<IEnumerable<string>>> GetTitleBooks();
        Task<IBaseResponse<IEnumerable<BookViewModel>>> GetBooks();
    }
}
