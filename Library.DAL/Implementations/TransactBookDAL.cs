using Dapper;
using Library.DAL.Interfaces;
using Library.DAL.Model;
using Library.Domain.ViewModels.Book;
using Library.Domain.ViewModels.Client;
using Library.Domain.ViewModels.TransactBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.DAL.Implementations
{
    public class TransactBookDAL : ITransactBookDAL
    {
        public async Task<bool> ExistBook(int bookId, int clientId)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QuerySingleAsync<bool>("SELECT CASE WHEN " +
                    "EXISTS(SELECT 1 FROM BookTransaction WHERE BookID = @bid AND " +
                    "ClientID = @cid AND ReturnDate IS NULL) THEN 1 ELSE 0 END",
                    new { bid = bookId, cid = clientId });
            }
        }

        public async Task<bool> QuantityReturnExist(int id, int count)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QuerySingleAsync<bool>("SELECT CASE WHEN " +
                    "@c > Quantity THEN 1 ELSE 0 END FROM BookTransaction " +
                    "WHERE BookTransactionID = @e",
                    new { e = id, c = count });
            }
        }

        public async Task GiveBook<TransactBook>(TransactBook book)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "INSERT INTO BookTransaction VALUES (@BookID, @ClientID, @RecieveDate, @ReturnDate, @Quantity)";
                await connection.ExecuteAsync(sql, book);
            }
        }

        public async Task<bool> QuantityExist(int id, int count)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QuerySingleAsync<bool>("SELECT CASE WHEN " +
                    "Quantity - @q > 0 THEN 1 ELSE 0 END FROM Books WHERE " +
                    "BookID = @e",
                    new { q = count, e = id });
            }
        }

        public async Task ReturnBook(int id, int quantity)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "UPDATE BookTransaction SET Quantity = Quantity - @q " +
                    "WHERE BookTransactionID = @e; UPDATE Books SET Quantity = " +
                    "Quantity + @q WHERE BookID = (SELECT BookID FROM BookTransaction " +
                    "WHERE BookTransactionID = @e)";
                await connection.ExecuteAsync(sql, new { q = quantity, e = id });
            }
        }

        public async Task<IEnumerable<TransactBookViewModel>> GetAllTransactBooks(int id)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QueryAsync<TransactBookViewModel>("SELECT BookTransactionID, Books.Title, Books.Author, BookTransaction.Quantity, RecieveDate FROM [BookTransaction] JOIN Books ON Books.BookID = BookTransaction.BookID WHERE ClientID = @e AND ReturnDate IS NULL",
                    new { e = id});
            }
        }
    }
}
