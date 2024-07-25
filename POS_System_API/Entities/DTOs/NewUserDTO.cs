using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class NewUserDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
