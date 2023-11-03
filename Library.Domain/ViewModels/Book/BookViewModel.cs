using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels.Book
{
    public class BookViewModel
    {
        public int BookID { get; set; }
        [Display(Name = "Название книги")]
        public string Title { get; set; }
        [Display(Name = "Автор")]
        public string Author { get; set; }
        [Display(Name = "Количество книг")]
        public int Quantity { get; set; }
        [Display(Name = "Описание")]
        public string Discription { get; set; }
    }
}
