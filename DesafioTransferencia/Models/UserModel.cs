using DesafioTransferencia.Enums;

namespace DesafioTransferencia.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }
        public UserType IsMerchant { get; set; }
    }
}
