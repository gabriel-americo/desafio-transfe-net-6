using DesafioTransferencia.Enums;
using DesafioTransferencia.Models;

namespace DesafioTransferencia.Services
{
    public class UserService
    {
        // Valida se o usuário é um lojista autorizado e se possui saldo suficiente para a transação.
        public void ValidateTransaction(UserModel user, decimal amount)
        {
            if (user.UserType != UserType.Merchant)
            {
                throw new Exception("Usúario do tipo lojista não está autorizado a fazer a transação");
            }

            if(user.WalletBalance.CompareTo(amount) < 0)
            {
                throw new Exception("Saldo Insuficiente");
            }
        }
    }
}
