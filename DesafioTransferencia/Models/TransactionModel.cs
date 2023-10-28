namespace DesafioTransferencia.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public decimal Value { get; set; }

        // Representa a chave estrangeira para o pagador (Payer)
        public Guid PayerId { get; set; }
        // Representa a chave estrangeira para o beneficiário (Payee)
        public Guid PayeeId { get; set; }

        public DateTime TransferDate { get; set; }
        public Enums.TransactionStatus Status { get; set; }
    }
}
