using DesafioTransferencia.Data;
using DesafioTransferencia.Enums;
using DesafioTransferencia.Models;
using DesafioTransferencia.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioTransferencia.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        private IUserRepository _userRepository;
        private HttpClient _httpClient;

        public TransactionRepository(AppDbContext context, IUserRepository userRepository, HttpClient httpClient)
        {
            _context = context;
            _userRepository = userRepository;
            _httpClient = httpClient;
        }

        public async Task<TransactionModel> GetTransactionById(int transactionId)
        {
            return await _context.Transaction.FindAsync(transactionId);
        }

        public async Task<IEnumerable<TransactionModel>> GetTransactionsByUserId(Guid userId)
        {
            return await _context.Transaction
             .Where(t => t.PayerId == userId || t.PayeeId == userId)
             .ToListAsync();
        }

        public async Task<IEnumerable<TransactionModel>> GetAllTransactions()
        {
            return await _context.Transaction.ToListAsync();
        }

        public async Task CreateTransaction(TransactionModel transaction)
        {
            var payer = await _userRepository.GetUserById(transaction.PayerId);
            if (payer == null)
            {
                throw new ArgumentException("Pagador não encontrado.");
            }

            var payee = await _userRepository.GetUserById(transaction.PayeeId);
            if (payee == null)
            {
                throw new ArgumentException("Beneficiário não encontrado.");
            }

            ValidateTransaction(payer, transaction.Value);

            /*bool isAuthorized = await AuthorizeTransaction(payer, transaction.Value);

            if (!isAuthorized)
            {
                throw new Exception("Transação não autorizada");
            }*/

            payer.WalletBalance -= transaction.Value;
            payee.WalletBalance += transaction.Value;

            await _userRepository.UpdateWalletBalance(payer.Id, payer.WalletBalance);
            await _userRepository.UpdateWalletBalance(payee.Id, payee.WalletBalance);

            var newTransaction = new TransactionModel
            {
                PayerId = payer.Id,
                PayeeId = payee.Id,
                Value = transaction.Value,
                TransferDate = DateTime.Today,
                Status = TransactionStatus.Completed
            };

            //_notificationService.sendNotification(payer, "Transação realizada com sucesso!");
            //_notificationService.sendNotification(payee, "Transação recebida com sucesso!");

            var entry = _context.Transaction.Add(newTransaction);
            await _context.SaveChangesAsync();
        }

        // Valida se o usuário é um lojista autorizado e se possui saldo suficiente para a transação.
        public void ValidateTransaction(UserModel user, decimal amount)
        {
            if (user.UserType != UserType.Merchant)
            {
                throw new Exception("Usúario do tipo lojista não está autorizado a fazer a transação");
            }

            if (user.WalletBalance.CompareTo(amount) < 0)
            {
                throw new Exception("Saldo Insuficiente");
            }
        }

        public async Task<bool> AuthorizeTransaction(UserModel payer, decimal value)
        {
            string externalServiceUrl = Environment.GetEnvironmentVariable("ExternalAuthorizationServiceUrl");

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(externalServiceUrl);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
