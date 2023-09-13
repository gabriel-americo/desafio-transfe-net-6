using DesafioTransferencia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioTransferencia.Data.Map
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(u => u.Id); // Chave primária
            builder.Property(u => u.Id).HasColumnName("UserId"); // Nome da coluna no banco de dados
            builder.Property(u => u.FullName).IsRequired();
            builder.Property(u => u.CPF).IsRequired().HasMaxLength(11); // Exemplo de tamanho máximo para CPF
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.WalletBalance).HasColumnType("decimal(18, 2)"); // Exemplo de tipo de dados para WalletBalance
            builder.Property(u => u.IsMerchant).IsRequired();
        }
    }
}
