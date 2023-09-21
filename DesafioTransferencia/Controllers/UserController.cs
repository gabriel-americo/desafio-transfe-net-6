﻿using DesafioTransferencia.Models;
using DesafioTransferencia.Repositories;
using DesafioTransferencia.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public async Task<ActionResult<UserModel>> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserModel>> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

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
            await _userRepository.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel user, int userId)
        {
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
        public async Task<IActionResult> DeleteUser(int userId)
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
