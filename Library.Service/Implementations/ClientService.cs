using Library.DAL.Implementations;
using Library.DAL.Interfaces;
using Library.DAL.Model;
using Library.Domain.Enum;
using Library.Domain.Response;
using Library.Domain.ViewModels.Book;
using Library.Domain.ViewModels.Client;
using Library.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientDAL _clientDAL;
        private ILogger<BookService> _logger;
        public ClientService(IClientDAL clientDAL, ILogger<BookService> logger)
        {
            _clientDAL = clientDAL;
            _logger = logger;
        }

        public async Task<IBaseResponse<Client>> Create(CreateClientViewModel model)
        {
            try
            {
                model.Validate();

                _logger.LogInformation($"Запрос на создание клиента - {model.SecondName} {model.Name}");

                var exist = await _clientDAL.ClientExist(model);

                if (exist)
                {
                    return new BaseResponse<Client>()
                    {
                        Description = "Такой клиент уже есть",
                        StatusCode = StatusCode.ClientHasAlready
                    };
                }

                Client client = new Client()
                {
                    SecondName = model.SecondName,
                    Name = model.Name,
                    FullName = model.FullName
                };

                await _clientDAL.Create(client);

                _logger.LogInformation($"Клиент создался - {model.SecondName} {model.Name}");
                return new BaseResponse<Client>()
                {
                    Description = "Клиент успешно создался",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ClientService.Create]: {ex.Message}");
                return new BaseResponse<Client>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<ClientViewModel>>> GetClients()
        {
            try
            {
                var clients = await _clientDAL.GetAllClients();

                return new BaseResponse<IEnumerable<ClientViewModel>>()
                {
                    Data = clients,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ClientService.GetClients]: {ex.Message}");
                return new BaseResponse<IEnumerable<ClientViewModel>>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
