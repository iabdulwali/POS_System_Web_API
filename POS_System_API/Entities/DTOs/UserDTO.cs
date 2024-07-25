using System.ComponentModel.DataAnnotations;

namespace POS_System_API.Entities.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
