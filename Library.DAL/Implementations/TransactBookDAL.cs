using Dapper;
using Library.DAL.Interfaces;
using Library.DAL.Model;
using Library.Domain.ViewModels.Book;
using Library.Domain.ViewModels.Client;
using Library.Domain.ViewModels.TransactBook;
using Microsoft.Data.SqlClient;
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
        SqlCommand cmd;
        SqlDataReader reader;

        public async Task<bool> ExistBook(int bookId, int clientId)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT CASE WHEN " +
                    "EXISTS(SELECT 1 FROM BookTransaction WHERE BookID = @bid AND " +
                    "ClientID = @cid AND ReturnDate IS NULL) THEN 1 ELSE 0 END", connection);
                cmd.Parameters.AddWithValue("@bid", bookId);
                cmd.Parameters.AddWithValue("@cid", clientId);
                await connection.OpenAsync();

                return Convert.ToBoolean(await cmd.ExecuteScalarAsync());
            }
        }

        public async Task<bool> QuantityReturnExist(int id, int count)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT CASE WHEN " +
                    "@c > Quantity THEN 1 ELSE 0 END FROM BookTransaction " +
                    "WHERE BookTransactionID = @e", connection);
                cmd.Parameters.AddWithValue("@e", id);
                cmd.Parameters.AddWithValue("@c", count);
                await connection.OpenAsync();

                return Convert.ToBoolean(await cmd.ExecuteScalarAsync());
            }
        }

        public async Task GiveBook(TransactBook book)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "INSERT INTO BookTransaction VALUES (@BookID, @ClientID, @RecieveDate, @ReturnDate, @Quantity)";
                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@BookID", book.BookID);
                cmd.Parameters.AddWithValue("@ClientID", book.ClientID);
                cmd.Parameters.AddWithValue("@RecieveDate", book.RecieveDate);
                cmd.Parameters.AddWithValue("@ReturnDate", book.ReturnDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Quantity", book.Quantity);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> QuantityExist(int id, int count)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT CASE WHEN " +
                    "Quantity - @q > 0 THEN 1 ELSE 0 END FROM Books WHERE " +
                    "BookID = @e", connection);
                cmd.Parameters.AddWithValue("@e", id);
                cmd.Parameters.AddWithValue("@q", count);
                
                await connection.OpenAsync();
                return Convert.ToBoolean(await cmd.ExecuteScalarAsync());
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
                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@q", quantity);
                cmd.Parameters.AddWithValue("@e", id);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<TransactBookViewModel>> GetAllTransactBooks(int id)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT BookTransactionID, Books.Title, " +
                    "Books.Author, BookTransaction.Quantity, RecieveDate FROM " +
                    "[BookTransaction] JOIN Books ON Books.BookID = BookTransaction.BookID " +
                    "WHERE ClientID = @e AND ReturnDate IS NULL", connection);
                cmd.Parameters.AddWithValue("@e", id);
                await connection.OpenAsync();

                using (reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.Parse<TransactBookViewModel>().ToList();
                }
            }
        }
    }
}
