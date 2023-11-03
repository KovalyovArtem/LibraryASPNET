using Dapper;
using Library.DAL.Interfaces;
using Library.DAL.Model;
using Library.Domain.ViewModels.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Implementations
{
    public class BookDAL : IBookDAL
    {
        public async Task<IEnumerable<BookViewModel>> GetAllBooks()
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QueryAsync<BookViewModel>("SELECT * FROM [Books]");
            }
        }

        public async Task<Book> GetBookByID(int id)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Book>("SELECT * FROM [Books] WHERE BookID = @e",
                    new { e = id });
            }
        }

        public async Task<Book> GetBookByTitle(string Title)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Book>("SELECT * FROM [Books] WHERE Title = @e",
                    new { e = Title });
            }
        }

        public async Task<IEnumerable<Book>> GetBookByTitleI(string Title)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QueryAsync<Book>("SELECT * FROM [Books] WHERE Title = @t",
                    new { t = Title });
            }
        }

        public async Task<int> GetBookIDByTitle(string Title)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return Convert.ToInt32(await connection.QueryFirstOrDefaultAsync<Book>("SELECT BookID FROM [Books] WHERE Title = @t",
                    new { t = Title }));
            }
        }

        public async Task<IEnumerable<string>> GetTitleOfBooks()
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QueryAsync<string>("SELECT Title FROM [Books]");
            }
        }

        public async Task Create<Book>(Book book)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "INSERT INTO Books VALUES (@Title, @Author, @Quantity, @Discription)";
                await connection.ExecuteAsync(sql, book);
            }
        }

        public async Task UpdateDiscripton(string title, string discription)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "UPDATE Books SET Discription = @Discription WHERE Title = @t";
                await connection.ExecuteAsync(sql, new { t = title, Discription = discription });
            }
        }

        public async Task UpdateDiscripton(int id, string discription)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "UPDATE Books SET Discription = @Discription WHERE BookID = @e";
                await connection.ExecuteAsync(sql, new { e = id, Discription = discription });
            }
        }
    }
}