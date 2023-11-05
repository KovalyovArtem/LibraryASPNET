using Dapper;
using Library.DAL.Interfaces;
using Library.DAL.Model;
using Library.Domain.ViewModels.Book;
using Library.Domain.ViewModels.Client;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Implementations
{
    public class ClientDAL : IClientDAL
    {
        SqlCommand cmd;
        SqlDataReader reader;

        public async Task<bool> ClientExist(CreateClientViewModel client)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT CASE WHEN EXISTS " +
                    "(SELECT 1 FROM Clients WHERE SecondName = @sc AND " +
                    "Name = @n AND FullName = @fn) THEN 1 ELSE 0 END", connection);
                cmd.Parameters.AddWithValue("@sc", client.SecondName);
                cmd.Parameters.AddWithValue("@n", client.Name);
                cmd.Parameters.AddWithValue("@fn", client.FullName);
                await connection.OpenAsync();

                return Convert.ToBoolean(await cmd.ExecuteScalarAsync());
            }
        }

        public async Task Create(Client client)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "INSERT INTO Clients VALUES (@SecondName, @Name, @FullName)";
                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@SecondName", client.SecondName);
                cmd.Parameters.AddWithValue("@Name", client.Name);
                cmd.Parameters.AddWithValue("@FullName", client.FullName);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<ClientViewModel>> GetAllClients()
        {
            using (var connection = DBConnection.CreateConnection())
            {
                cmd = new SqlCommand("SELECT * FROM [Clients]", connection);
                await connection.OpenAsync();

                using (reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.Parse<ClientViewModel>().ToList();
                }
            }
        }
    }
}
