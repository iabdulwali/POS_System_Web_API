using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Models;

namespace POS_System_API.Entities.Mappings
{
    public static class UserMapping
    {
        public static UserDTO ConvertToUserDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Phone = user.Phone
            };
        }

        public static List<UserDTO> ConvertToUserDTO(this List<User> users)
        {
            return users.Select(user => user.ConvertToUserDTO()).ToList();
        }

        public static User ToUser(this CreateUserDTO createUserDTO)
        {
            return new User
            {
                Username = createUserDTO.Username,
                FullName = createUserDTO.FullName,
                Phone = createUserDTO.Phone,
                Password = createUserDTO.Password
            };
        }

        public static void ToUser(this CreateUserDTO createUserDTO, User user)
        {
            user.Username = createUserDTO.Username;
            user.FullName = createUserDTO.FullName;
            user.Phone = createUserDTO.Phone;
            user.Password = createUserDTO.Password;
            user.UpdatedDate = DateTime.Now;
        }

        public static void ToUser(this UserDTO userDTO, User user)
        {
            user.Username = userDTO.Username;
            user.FullName = userDTO.FullName;
            user.Phone = userDTO.Phone;
            user.UpdatedDate = DateTime.Now;
        }
    }
}
