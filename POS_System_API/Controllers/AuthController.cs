using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS_System_API.Entities.DTOs;
using POS_System_API.Repositories.Implementations;
using POS_System_API.Repositories.Interfaces;
using POS_System_API.Token;

namespace POS_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<NewUserDTO>> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newUserDTO = await _authRepository.register(registerDTO);

                if (newUserDTO != null)
                {
                    return Ok(newUserDTO);
                }
                else
                {
                    return BadRequest("Account creation has failed!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _authRepository.login(loginDTO);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return Unauthorized("Invalid credentials!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
