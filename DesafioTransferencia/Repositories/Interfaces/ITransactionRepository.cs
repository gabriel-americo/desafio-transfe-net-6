using DesafioTransferencia.Models;

namespace DesafioTransferencia.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<TransactionModel> GetTransactionById(int transactionId);
        Task<IEnumerable<TransactionModel>> GetTransactionsByUserId(Guid userId);
        Task<IEnumerable<TransactionModel>> GetAllTransactions();
        Task CreateTransaction(TransactionModel transaction);
    }
}
