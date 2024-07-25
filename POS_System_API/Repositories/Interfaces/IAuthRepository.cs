using POS_System_API.Entities.DTOs;

namespace POS_System_API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        public Task<NewUserDTO?> register(RegisterDTO registerDTO);
        public Task<NewUserDTO?> login(LoginDTO loginDTO);
    }
}
