﻿using DesafioTransferencia.Models;
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

        [HttpGet("id/{userId}")]
        public async Task<ActionResult<UserModel>> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(user);
        }

        [HttpGet("document/{document}")]
        public async Task<ActionResult<UserModel>> GetUserDocument(string document)
        {
            var user = await _userRepository.GetUserByDocument(document);

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
            if(user == null)
            {
                return BadRequest();
            }
            await _userRepository.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel user, Guid userId)
        {
            try
            {
                await _userRepository.UpdateUser(userId, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro ao atualizar o usuário.");
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
                return BadRequest("Ocorreu um erro ao deletar o usuário.");
            }
        }
    }
}
