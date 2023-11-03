using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels.Client
{
    public class CreateClientViewModel
    {
        public string SecondName { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(SecondName))
                throw new ArgumentNullException(SecondName, "Укажите фамилию");
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException(Name, "Укажите имя");
        }
    }
}
