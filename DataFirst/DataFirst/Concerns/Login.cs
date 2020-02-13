using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarPoolApplication.Concerns
{
    public class Login
    {
        [Display(Name ="Username")]
        [Required]
        public string Username { get; set; }

        [Display(Name ="Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
