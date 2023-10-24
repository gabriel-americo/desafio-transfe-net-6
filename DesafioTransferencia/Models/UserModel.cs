using DesafioTransferencia.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DesafioTransferencia.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Document { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }
        public UserType UserType { get; set; }
    }
}
