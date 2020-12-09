using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Enums;

namespace WebApplication.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public ContactType Type { get; set; }
        public string Value { get; set; }
        public int PersonId { get; set; }


        public virtual Person Person { get; set; }
    }
}
