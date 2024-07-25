using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Mappings;
using POS_System_API.Entities.Models;
using POS_System_API.Repositories.Interfaces;
using POS_System_API.Token;

namespace POS_System_API.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthRepository(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) 
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<NewUserDTO?> register(RegisterDTO registerDTO) 
        {
            if(registerDTO == null || registerDTO.Password == null)
            {
                return null;
            }

            var user = registerDTO.ConvertToAppUser();

            var createdUser = await _userManager.CreateAsync(user, registerDTO.Password);

            if(createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");

                if(roleResult.Succeeded && user.UserName != null && user.Email != null)
                {
                    return new NewUserDTO
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<NewUserDTO?> login(LoginDTO loginDTO)
        {
            var user = _userManager.Users.FirstOrDefault(user => user.UserName == loginDTO.UserName);

            if(user == null || user.UserName == null || user.Email == null)
            {
                return null; 
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded)
            {
                return null;
            }

            return new NewUserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
