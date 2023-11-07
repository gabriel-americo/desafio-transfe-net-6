using DesafioTransferencia.Data;
using DesafioTransferencia.Models;
using DesafioTransferencia.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioTransferencia.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> GetUserById(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<UserModel> GetUserByDocument(string document)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Document == document);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task CreateUser(UserModel user)
        {
            if (await IsDocumentUnique(user.Document))
            {
                throw new Exception("Documento já cadastrado.");
            }

            if (await IsEmailUnique(user.Email))
            {
                throw new Exception("E-mail já cadastrado.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateUser(Guid userId, UserModel user)
        {
            var existingUser = await GetUserById(userId);

            if (existingUser == null)
            {
                throw new Exception($"Usuario para o id: {userId} não foi encontrado no banco de dados");
            }

            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.Document = user.Document;
            existingUser.WalletBalance = user.WalletBalance;
            existingUser.UserType = user.UserType;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var user = await GetUserById(userId);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateWalletBalance(Guid userId, decimal newBalance)
        {
            var user = await GetUserById(userId);

            if (user == null)
            {
                throw new Exception($"Usuário com o id: {userId} não encontrado no banco de dados");
            }

            user.WalletBalance = newBalance;

            await _context.SaveChangesAsync();
            return true;
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
