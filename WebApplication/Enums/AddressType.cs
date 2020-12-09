using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Enums
{
    public enum AddressType
    {
        [Display (Name ="Residencial")]
        Residential,
        [Display(Name = "Comercial")]
        Comercial
    }
}
