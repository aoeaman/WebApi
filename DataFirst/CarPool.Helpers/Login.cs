using System.ComponentModel.DataAnnotations;

namespace CarPool.Helpers
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
