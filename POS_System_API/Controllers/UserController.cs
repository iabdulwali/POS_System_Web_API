using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using POS_System_API.Entities.DTOs;
using POS_System_API.Helpers;
using POS_System_API.Repositories.Interfaces;

namespace POS_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserDTO>>> GetAll([FromQuery] UserQueryObject query)
        {
            var users = await _userRepository.getAll(query);
            return Ok(users);
        }

        [HttpGet("{id:Guid}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var userDTO = await _userRepository.getById(id);

            if (userDTO == null)
            {
                return NotFound();
            }
            return Ok(userDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> Create([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createUserDTO == null)
            {
                return BadRequest();
            }

            var userDTO = await _userRepository.create(createUserDTO);
            
            return Ok(userDTO);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var userDTO = await _userRepository.delete(id);

            if (userDTO == null)
            {
                return NotFound();
            }

            return Ok(userDTO);
        }

        [HttpPut("{id:Guid}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> Update([FromRoute] Guid id, [FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == Guid.Empty || createUserDTO == null)
            {
                return BadRequest();
            }

            var updatedUserDTO = await _userRepository.update(id, createUserDTO);

            if (updatedUserDTO == null)
            {
                return NotFound();
            }

            return Ok(updatedUserDTO);
        }

        [HttpPatch("{id:Guid}", Name = "UpdatePartialUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> UpdatePartial(Guid id, JsonPatchDocument<UserDTO> patchDTO)
        {
            if (patchDTO == null || id == Guid.Empty)
            {
                return BadRequest();
            }

            var userDTO = await _userRepository.updatePartial(id, patchDTO);

            if (userDTO == null)
            {
                return NotFound();
            }

            return Ok(userDTO);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<object>> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (loginDTO == null || loginDTO.UserName == String.Empty || loginDTO.Password == String.Empty)
            {
                return BadRequest();
            }

            var result = await _userRepository.login(loginDTO);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
