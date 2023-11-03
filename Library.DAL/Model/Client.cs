using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Model
{
    public class Client
    {
        public int ClientID { get; set; }
        public string SecondName { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}
