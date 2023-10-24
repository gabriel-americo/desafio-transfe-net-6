using DesafioTransferencia.Models;

namespace DesafioTransferencia.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserById(Guid userId);
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task CreateUser(UserModel user);
        Task UpdateUser(UserModel user, Guid id);
        Task DeleteUser(Guid userId);
        Task<bool> IsDocumentUnique(string document);
        Task<bool> IsEmailUnique(string email);
    }
}
