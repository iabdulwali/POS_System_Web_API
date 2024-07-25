using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Models;

namespace POS_System_API.Entities.Mappings
{
    public static class AuthMapping
    {
        public static AppUser ConvertToAppUser(this RegisterDTO registerDTO)
        {
            return new AppUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };
        }
    }
}
