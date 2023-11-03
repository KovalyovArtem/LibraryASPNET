using Dapper;
using Library.DAL.Interfaces;
using Library.DAL.Model;
using Library.Domain.ViewModels.Book;
using Library.Domain.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Implementations
{
    public class ClientDAL : IClientDAL
    {
        public async Task<bool> ClientExist(CreateClientViewModel client)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QuerySingleAsync<bool>("SELECT CASE WHEN EXISTS " +
                    "(SELECT 1 FROM Clients WHERE SecondName = @sc AND " +
                    "Name = @n AND FullName = @fn) THEN 1 ELSE 0 END",
                    new { sc = client.SecondName, n = client.Name, fn = client.FullName });
            }
        }

        public async Task Create<Client>(Client client)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var sql = "INSERT INTO Clients VALUES (@SecondName, @Name, @FullName)";
                await connection.ExecuteAsync(sql, client);
            }
        }

        public async Task<IEnumerable<ClientViewModel>> GetAllClients()
        {
            using (var connection = DBConnection.CreateConnection())
            {
                return await connection.QueryAsync<ClientViewModel>("SELECT * FROM [Clients]");
            }
        }
    }
}
