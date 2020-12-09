using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Enums;

namespace WebApplication.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public int? Number { get; set; }

        public string Complement { get; set; }

        public AddressType AddressType { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }


    }
}
