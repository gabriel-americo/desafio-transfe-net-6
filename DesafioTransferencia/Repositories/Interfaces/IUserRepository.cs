using DesafioTransferencia.Models;

namespace DesafioTransferencia.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserById(Guid userId);
        Task<UserModel> GetUserByDocument(string document);
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task CreateUser(UserModel user);
        Task<bool> UpdateUser(Guid userId, UserModel user);
        Task<bool> DeleteUser(Guid userId);
        Task<bool> IsDocumentUnique(string document);
        Task<bool> IsEmailUnique(string email);
    }
}
