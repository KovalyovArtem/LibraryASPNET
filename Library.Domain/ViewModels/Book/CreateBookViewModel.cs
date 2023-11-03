using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Domain.ViewModels.Book
{
    public class CreateBookViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public string Discription { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
                throw new ArgumentNullException(Title, "Укажите название книги");
            if (string.IsNullOrWhiteSpace(Author))
                throw new ArgumentNullException(Author, "Укажите автора книги");
            if (string.IsNullOrWhiteSpace(Discription))
                throw new ArgumentNullException(Discription, "Укажите описание книги");
        }
    }
}
