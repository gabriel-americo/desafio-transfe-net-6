﻿using DesafioTransferencia.Enums;

namespace DesafioTransferencia.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }
        public UserType UserType { get; set; }
    }
}
