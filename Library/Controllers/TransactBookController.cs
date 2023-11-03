using Library.DAL.Model;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Library.Controllers
{
    public class TransactBookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ITransactBookService _transactBookService;
        private static int ClientID;
        public TransactBookController(IBookService bookService, ITransactBookService transactBookService)
        {
            _bookService = bookService;
            _transactBookService = transactBookService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ClientID = Convert.ToInt32(Request.Query["id"]);
            return View();
        }

        public async Task<IActionResult> GiveBook(int id, int quantity)
        {
            var response = await _transactBookService.GiveBook(id, ClientID, quantity);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        public async Task<IActionResult> TaskHandler()
        {
            var response = await _bookService.GetBooks();
            return Json(new { data = response.Data });
        }
    }
}
