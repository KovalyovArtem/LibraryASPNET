using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Model
{
    public class TransactBook
    {
        public int BookTransactionID { get; set; }
        public int BookID { get; set; }
        public int ClientID { get; set; }
        public int Quantity { get; set; }
        public DateTime RecieveDate { get; set; }
        public DateTime? ReturnDate { get; set; } = null;
    }
}
