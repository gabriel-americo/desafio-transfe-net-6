using DesafioTransferencia.Models;

namespace DesafioTransferencia.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserById(int userId);
        Task<UserModel> GetUserByEmail(string email);
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task CreateUser(UserModel user);
        Task UpdateUser(UserModel user, int id);
        Task DeleteUser(int userId);
    }
}
