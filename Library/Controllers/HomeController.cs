using Library.Domain.ViewModels.Book;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;

        public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookViewModel model)
        {
            var response = await _bookService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(string Title, string Discription)
        {
            var response = await _bookService.UpdateDiscription(Title, Discription);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHandler(int Id, string Discription)
        {
            var response = await _bookService.UpdateDiscription(Id, Discription);
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