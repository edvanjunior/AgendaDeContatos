using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Enums;

namespace WebApplication.ViewModels
{
    public class CreateContactViewModel
    {
        [Required]
        public ContactType Type { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public int PersonId { get; set; }
    }
}
