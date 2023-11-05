using Library.DAL.Model;
using Library.Domain.ViewModels.Client;

namespace Library.DAL.Interfaces
{
    public interface IClientDAL
    {
        Task Create(Client client);
        Task<bool> ClientExist(CreateClientViewModel client);
        Task<IEnumerable<ClientViewModel>> GetAllClients();
    }
}
