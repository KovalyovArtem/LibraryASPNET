using Library.DAL.Model;
using Library.Domain.ViewModels.Book;
using Library.Domain.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Interfaces
{
    public interface IClientDAL
    {
        Task Create<Client>(Client client);
        Task<bool> ClientExist(CreateClientViewModel client);
        Task<IEnumerable<ClientViewModel>> GetAllClients();
    }
}
