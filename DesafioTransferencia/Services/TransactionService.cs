using DesafioTransferencia.Models;
using DesafioTransferencia.Repositories;
using DesafioTransferencia.Enums;

namespace DesafioTransferencia.Services
{
    public class TransactionService
    {
        private UserRepository _userRepository;
        private UserService _userService;
        private HttpClient _httpClient;
        private TransactionRepository _transactionRepository;
        private NotificationService _notificationService;

        public TransactionService(UserRepository userRepository, UserService userService, HttpClient httpClient, TransactionRepository transactionRepository, NotificationService notificationService)
        {
            _userRepository = userRepository;
            _httpClient = httpClient;
            _transactionRepository = transactionRepository;
            _userService = userService;
            _notificationService = notificationService;
        }

        public async Task<TransactionModel> GetTransactionById(int transactionId)
        {
            return await _transactionRepository.GetTransactionById(transactionId);
        }

        public async Task<TransactionModel> CreateTransaction(TransactionModel transaction)
        {
            // Verifique se o pagador (Payer) existe
            var payer = await _userRepository.GetUserById(transaction.PayerId);
            if (payer == null)
            {
                throw new ArgumentException("Pagador não encontrado.");
            }

            // Verifique se o beneficiário (Payee) existe
            var payee = await _userRepository.GetUserById(transaction.PayeeId);
            if (payee == null)
            {
                throw new ArgumentException("Beneficiário não encontrado.");
            }

            // Valide o saldo do pagador antes de criar a transação
            if (payer.WalletBalance < transaction.Value)
            {
                throw new InvalidOperationException("Saldo insuficiente.");
            }

            _userService.ValidateTransaction(payer, transaction.Value);

            bool isAuthorized = await AuthorizeTransaction(payer, transaction.Value);

            if (!isAuthorized)
            {
                throw new Exception("Transação não autorizada");
            }

            var newTransaction = new TransactionModel
            {
                Value = transaction.Value,
                PayerId = payer.Id,
                PayeeId = payee.Id,
                TransferDate = DateTime.Now,
                Status = TransactionStatus.Completed
            };

            payer.WalletBalance -= transaction.Value;
            await _userRepository.CreateUser(payer);

            payee.WalletBalance += transaction.Value;
            await _userRepository.CreateUser(payee);

            // Registre a transação no banco de dados
            var createdTransaction = await _transactionRepository.CreateTransaction(newTransaction);

            //_notificationService.sendNotification(payer, "Transação realizada com sucesso!");
            //_notificationService.sendNotification(payee, "Transação recebida com sucesso!");

            return createdTransaction;
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
