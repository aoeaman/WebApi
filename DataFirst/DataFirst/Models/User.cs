using System.ComponentModel.DataAnnotations;

namespace CarPoolApplication.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public char Gender { get; set; }
        public byte Age { get; set; }
        public string PhoneNumber { get; set; }
    }
}
