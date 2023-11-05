using Dapper;
using Library.DAL.Interfaces;
using Library.DAL.Model;
using Library.Domain.ViewModels.Book;
using Microsoft.Data.SqlClient;

namespace Library.DAL.Implementations
{
    public class BookDAL : IBookDAL
    {
        SqlCommand cmd;
        SqlDataReader reader;

        public async Task<IEnumerable<BookViewModel>> GetAllBooks()
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT * FROM [Books]", connection);
                await connection.OpenAsync();

                using (reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.Parse<BookViewModel>().ToList();
                }
            }
        }

        public async Task<Book> GetBookByID(int id)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT * FROM [Books] WHERE BookID = @e", connection);
                cmd.Parameters.AddWithValue("@e", id);
                await connection.OpenAsync();

                using (reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.Parse<Book>().FirstOrDefault();
                }
            }
        }

        public async Task<Book> GetBookByTitle(string Title)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT * FROM [Books] WHERE Title = @e", connection);
                cmd.Parameters.AddWithValue("@e", Title);
                await connection.OpenAsync();

                using (reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.Parse<Book>().FirstOrDefault();
                }
            }
        }

        public async Task<IEnumerable<Book>> GetBookByTitleI(string Title)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT * FROM [Books] WHERE Title = @t", connection);
                cmd.Parameters.AddWithValue("@t", Title);
                await connection.OpenAsync();

                using (reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.Parse<Book>().ToList();
                }
            }
        }

        public async Task<int> GetBookIDByTitle(string Title)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT BookID FROM [Books] WHERE Title = @t", connection);
                cmd.Parameters.AddWithValue("@t", Title);
                await connection.OpenAsync();

                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }

        public async Task<IEnumerable<string>> GetTitleOfBooks()
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT Title FROM [Books]", connection);
                await connection.OpenAsync();

                using (reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.Parse<string>().ToList();
                }
            }
        }

        public async Task Create(Book book)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "INSERT INTO Books VALUES (@Title, @Author, @Quantity, @Discription)";
                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                cmd.Parameters.AddWithValue("@Discription", book.Discription);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateDiscripton(string title, string discription)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "UPDATE Books SET Discription = @Discription WHERE Title = @Title";
                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Discription", discription);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateDiscripton(int id, string discription)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "UPDATE Books SET Discription = @Discription WHERE BookID = @e";
                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@e", id);
                cmd.Parameters.AddWithValue("@Discription", discription);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}