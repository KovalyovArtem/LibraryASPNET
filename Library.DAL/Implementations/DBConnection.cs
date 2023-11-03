using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Implementations
{
    public class DBConnection
    {
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection("Server=ARTYOMPC;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=true");
        }
    }
}
