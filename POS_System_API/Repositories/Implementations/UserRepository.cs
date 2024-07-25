using Microsoft.AspNetCore.Mvc;
using POS_System_API.Data;
using POS_System_API.Entities.DTOs;
using POS_System_API.Repositories.Interfaces;
using POS_System_API.Entities.Mappings;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using POS_System_API.Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using POS_System_API.Helpers;

namespace POS_System_API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration) 
        { 
            _db = db;
            _configuration = configuration;
        }

        public async Task<List<UserDTO>> getAll(UserQueryObject query)
        {
            var users = _db.Users.AsQueryable();
            
            if(!string.IsNullOrWhiteSpace(query.Username))
                users = users.Where(user => user.Username.Contains(query.Username));

            if (!string.IsNullOrWhiteSpace(query.FullName))
                users = users.Where(user => user.FullName.Contains(query.FullName));

            if (!string.IsNullOrWhiteSpace(query.Phone))
                users = users.Where(user => user.Phone.Contains(query.Phone));

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Username", StringComparison.OrdinalIgnoreCase))
                {
                    users = query.isDescending ? users.OrderByDescending(user => user.Username) : users.OrderBy(user => user.Username);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            var usersList = await users.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            return usersList.ConvertToUserDTO();
        }

        public async Task<UserDTO?> getById(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return null;
            }

            return user.ConvertToUserDTO();
        }

        public async Task<UserDTO?> create(CreateUserDTO createUserDTO)
        {
            var newUser = createUserDTO.ToUser();
            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == newUser.Id);

            if (user == null) 
            {
                return null;
            }

            return user.ConvertToUserDTO();
        }

        public async Task<UserDTO?> update(Guid id, CreateUserDTO createUserDTO)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return null;
            }

            createUserDTO.ToUser(user);

            await _db.SaveChangesAsync();

            return user.ConvertToUserDTO();
        }

        public async Task<UserDTO?> updatePartial(Guid id, JsonPatchDocument<UserDTO> patchDTO)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Id == id);

            if(user == null)
            {
                return null;
            }

            var userDTO = user.ConvertToUserDTO();
            patchDTO.ApplyTo(userDTO);
            userDTO.ToUser(user);
            await _db.SaveChangesAsync();

            return userDTO;
        }

        public async Task<UserDTO?> delete(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return user.ConvertToUserDTO();
        }

        public async Task<object?> login(LoginDTO loginDTO)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == loginDTO.UserName && u.Password == loginDTO.Password);

            if (user == null) 
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Username", user.Username.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"], 
                _configuration["Jwt:Audience"], 
                claims, 
                expires: DateTime.UtcNow.AddMinutes(60), 
                signingCredentials: signin
            );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return new { Token = tokenValue, User = user.ConvertToUserDTO() };
        }
    }
}
