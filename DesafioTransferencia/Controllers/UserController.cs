using DesafioTransferencia.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DesafioTransferencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _context;

        public UserController(UserRepository context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Dados inválidos.");
            }

            // Verifica se o CPF ou e-mail já existem no banco de dados.
            if (await _context.Users.AnyAsync(u => u.CPF == user.CPF || u.Email == user.Email))
            {
                return Conflict("CPF ou e-mail já cadastrados.");
            }

            // Hash da senha (não esqueça de implementar uma função segura para hash).
            user.Password = HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("Usuário criado com sucesso.");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Remova campos sensíveis, como senha, antes de retornar os dados.
            user.Password = null;

            return Ok(user);
        }

        // Outros endpoints para consultar e atualizar usuários.

        private string HashPassword(string password)
        {
            // Implemente uma função segura para hash de senha aqui.
            // Não armazene senhas em texto plano no banco de dados.
            // Use bibliotecas de hashing, como BCrypt ou ASP.NET Identity.
            throw new NotImplementedException();
        }
    }
}
