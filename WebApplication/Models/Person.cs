using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual List<Address> Addresses { get; set; }
        public virtual List<Contact> Contacts { get; set; }
    }
}
