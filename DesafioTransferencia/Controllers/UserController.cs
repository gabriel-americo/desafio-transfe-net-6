using DesafioTransferencia.Models;
using DesafioTransferencia.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTransferencia.Controllers
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

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserModel>> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna detalhes dos erros de validação ao cliente
            }

            await _userRepository.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel user, Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna detalhes dos erros de validação ao cliente
            }

            try
            {
                await _userRepository.UpdateUser(user, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                await _userRepository.DeleteUser(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
