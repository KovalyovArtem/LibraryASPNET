using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Domain.ViewModels.TransactBook
{
    public class GiveTransactBookViewModel
    {
        public int BookID { get; set; }
        public int ClientID { get; set; }
        public DateTime RecieveDate { get; set; }
    }
}
