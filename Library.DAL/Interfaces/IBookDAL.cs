using Library.DAL.Model;
using Library.Domain.ViewModels.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Interfaces
{
    public interface IBookDAL
    {
        Task Create<Book>(Book book);
        Task UpdateDiscripton(string title, string discription);
        Task UpdateDiscripton(int id, string discription);
        Task<Book> GetBookByID(int id);
        Task<Book> GetBookByTitle(string Title);
        Task<IEnumerable<Book>> GetBookByTitleI(string Title);
        Task<int> GetBookIDByTitle(string Title);
        Task<IEnumerable<string>> GetTitleOfBooks();
        Task<IEnumerable<BookViewModel>> GetAllBooks();
    }
}
