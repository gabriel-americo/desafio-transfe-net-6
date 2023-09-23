using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DesafioTransferencia.Data;
using DesafioTransferencia.Models;
using DesafioTransferencia.Repositories;
using DesafioTransferencia.Enums;
using DesafioTransferencia.Repositories.Interfaces;

namespace DesafioTransferencia.Tests
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            // Configuração da Connection String de teste
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Your_Test_Connection_String_Here") // Substitua pela sua Connection String de teste
                .Options;

            _context = new AppDbContext(options);
            _userRepository = new UserRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task CreateUser_ShouldCreateUser()
        {
            // Arrange
            var newUser = new UserModel
            {
                FullName = "Novo Usuário",
                CpfCnpj = "12345678901",
                Email = "novo@email.com",
                Password = "senha",
                WalletBalance = 100.0m,
                UserType = UserType.Common
            };

            // Act
            await _userRepository.CreateUser(newUser);

            // Assert
            var createdUser = await _context.Users.FindAsync(newUser.Id);
            Assert.NotNull(createdUser);
            Assert.Equal(newUser.FullName, createdUser.FullName);
            // Adicione outros asserts para verificar outros campos, se necessário
        }

        [Fact]
        public async Task CreateUser_ShouldThrowExceptionForDuplicateEmail()
        {
            // Arrange
            var existingUser = new UserModel
            {
                FullName = "Usuário Existente",
                CpfCnpj = "11111111111",
                Email = "emailexistente@email.com",
                Password = "senha",
                WalletBalance = 50.0m,
                UserType = UserType.Common
            };

            // Adicione o usuário existente ao contexto
            _context.Users.Add(existingUser);
            await _context.SaveChangesAsync();

            var newUser = new UserModel
            {
                FullName = "Novo Usuário",
                CpfCnpj = "12345678901",
                Email = "emailexistente@email.com", // E-mail duplicado
                Password = "senha",
                WalletBalance = 100.0m,
                UserType = UserType.Common
            };

            // Act e Assert
            await Assert.ThrowsAsync<Exception>(() => _userRepository.CreateUser(newUser));
        }

        // Adicione mais testes para outros cenários, como atualização, exclusão, consultas e assim por diante
    }
}
