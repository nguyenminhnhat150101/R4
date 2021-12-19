using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace R4ClothesServer.Models.ViewModels
{
    public class Login
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
