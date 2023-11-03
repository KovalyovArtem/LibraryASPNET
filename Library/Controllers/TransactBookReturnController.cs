using Library.DAL.Model;
using Library.Service.Implementations;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class TransactBookReturnController : Controller
    {
        private readonly ITransactBookService _transactBookService;
        private static int ClientID;
        public TransactBookReturnController(ITransactBookService transactBookService)
        {
            _transactBookService = transactBookService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ClientID = Convert.ToInt32(Request.Query["id"]);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(int id, int quantity)
        {
            var response = await _transactBookService.ReturnBook(id, quantity);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> TaskHandler()
        {
            var response = await _transactBookService.GetAllTransactBooksByID(ClientID);
            return Json(new { data = response.Data });
        }
    }
}
