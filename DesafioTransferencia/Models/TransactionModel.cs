using System.Transactions;

namespace DesafioTransferencia.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public decimal Value { get; set; }

        // Representa a chave estrangeira para o pagador (Payer)
        public int PayerId { get; set; }
        // Representa a chave estrangeira para o beneficiário (Payee)
        public int PayeeId { get; set; }

        public DateTime TransferDate { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
