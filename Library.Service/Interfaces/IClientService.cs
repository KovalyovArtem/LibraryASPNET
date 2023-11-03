using Library.DAL.Model;
using Library.Domain.Response;
using Library.Domain.ViewModels.Book;
using Library.Domain.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
    public interface IClientService
    {
        Task<IBaseResponse<Client>> Create(CreateClientViewModel model);
        Task<IBaseResponse<IEnumerable<ClientViewModel>>> GetClients();
    }
}
