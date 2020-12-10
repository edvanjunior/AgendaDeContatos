using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.ViewModels
{
    public class EditPersonViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
