using DesafioTransferencia.Data;
using DesafioTransferencia.Models;
using DesafioTransferencia.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioTransferencia.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext appContext)
        {
            _context = appContext;
        }

        public async Task<UserModel> GetUserById(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task CreateUser(UserModel user)
        {
            // Verificar unicidade do Documento
            if (await IsDocumentUnique(user.Document))
            {
                throw new Exception("Documento já cadastrado.");
            }

            // Verificar unicidade do e-mail
            if (await IsEmailUnique(user.Email))
            {
                throw new Exception("E-mail já cadastrado.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(UserModel user, Guid id)
        {
            UserModel userId = await GetUserById(id);

            if (userId == null)
            {
                throw new Exception($"Usuario para o id: {id} não foi encontrado no banco de dados");
            }
            
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsDocumentUnique(string document)
        {
            // Verificar se o CPF/CNPJ já existe no banco de dados
            return await _context.Users.AnyAsync(u => u.Document == document);
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            // Verificar se o e-mail já existe no banco de dados
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
