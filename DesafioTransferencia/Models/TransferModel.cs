namespace DesafioTransferencia.Models
{
    public class TransferModel
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int PayerId { get; set; }
        public int PayeeId { get; set; }
    }
}
