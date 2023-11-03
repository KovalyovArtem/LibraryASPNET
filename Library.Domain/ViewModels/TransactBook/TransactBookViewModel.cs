using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels.TransactBook
{
    public class TransactBookViewModel
    {
        public int BookTransactionID { get; set; }
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Display(Name = "Автор")]
        public string Author { get; set; }
        [Display(Name = "Количество")]
        public string Quantity { get; set; }
        [Display(Name = "Дата выдачи")]
        public DateTime RecieveDate { get; set; }
    }
}
