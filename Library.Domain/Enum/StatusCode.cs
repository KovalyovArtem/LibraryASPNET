using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Enum
{
    public enum StatusCode
    {
        DBError = 1,

        BookHasAlready = 2,
        BookHasNotExist = 3,

        ClientHasAlready = 4,

        TransactionBookHasAlready = 5,
        QuantityError = 6,
        QuantityMoreThenHave = 7,

        OK = 200,
        InternalServerError = 500
    }
}
