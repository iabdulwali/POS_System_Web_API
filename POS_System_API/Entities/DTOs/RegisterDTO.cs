using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public String? Username { get; set; }
        [Required]
        [EmailAddress]
        public String? Email { get; set; }
        [Required]
        public String? Password { get; set; }
    }
}
