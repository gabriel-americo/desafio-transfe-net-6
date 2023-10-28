using DesafioTransferencia.Enums;

namespace DesafioTransferencia.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public Guid PayerId { get; set; }
        public Guid PayeeId { get; set; }
        public DateTime TransferDate { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
