using Microsoft.AspNetCore.JsonPatch;
using POS_System_API.Entities.DTOs;
using POS_System_API.Helpers;

namespace POS_System_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<UserDTO>> getAll(UserQueryObject query);
        public Task<UserDTO?> getById(Guid id);
        public Task<UserDTO?> create(CreateUserDTO createUserDTO);
        public Task<UserDTO?> update(Guid id, CreateUserDTO createUserDTO);
        public Task<UserDTO?> updatePartial(Guid id, JsonPatchDocument<UserDTO> patchDTO);
        public Task<UserDTO?> delete(Guid id);
        public Task<object?> login(LoginDTO loginDTO);
    }
}
