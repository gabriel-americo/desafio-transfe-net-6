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

        public async Task<UserModel> GetUserById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task CreateUser(UserModel user)
        {
            // Verificar unicidade do CPF/CNPJ
            if (await IsCpfCnpjUnique(user.CpfCnpj))
            {
                throw new Exception("CPF/CNPJ já cadastrado.");
            }

            // Verificar unicidade do e-mail
            if (await IsEmailUnique(user.Email))
            {
                throw new Exception("E-mail já cadastrado.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(UserModel user, int id)
        {
            UserModel userId = await GetUserById(id);

            if (userId == null)
            {
                throw new Exception($"Usuario para o id: {id} não foi encontrado no banco de dados");
            }
            
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsCpfCnpjUnique(string cpfCnpj)
        {
            // Verificar se o CPF/CNPJ já existe no banco de dados
            return !await _context.Users.AnyAsync(u => u.CpfCnpj == cpfCnpj);
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            // Verificar se o e-mail já existe no banco de dados
            return !await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
