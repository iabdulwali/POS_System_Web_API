using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
