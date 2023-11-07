using DesafioTransferencia.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioTransferencia.Models
{
    [Table("Users")]
    public class UserModel
    {
        [Column("Id")]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }
        public UserType UserType { get; set; }
    }
}
