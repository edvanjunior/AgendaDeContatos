using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Enums;

namespace WebApplication.ViewModels
{
    public class CreateAddressViewModel
    {
        [Required]
        public string Location { get; set; }

        public int? Number { get; set; }
        [Required]
        public string Complement { get; set; }
        [Required]
        public AddressType AddressType { get; set; }
        [Required]
        public string Neighborhood { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int PersonId { get; set; }
    }
}
