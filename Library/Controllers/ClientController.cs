using Library.Domain.ViewModels.Book;
using Library.Domain.ViewModels.Client;
using Library.Service.Implementations;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientViewModel model)
        {
            var response = await _clientService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        public async Task<IActionResult> TaskHandler()
        {
            var response = await _clientService.GetClients();
            return Json(new { data = response.Data });
        }
    }
}
