using System.ComponentModel.DataAnnotations;

namespace CarPoolApplication.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }       
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public char Gender { get; set; }
        public byte Age { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
