using DesafioTransferencia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioTransferencia.Data.Map
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder
            .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .HasColumnName("Id");

            builder
                .Property(u => u.FullName)
                .IsRequired();

            builder
                .Property(u => u.CpfCnpj)
                .IsRequired()
                .HasMaxLength(14);

            builder
                .Property(u => u.Email)
                .IsRequired();

            builder
                .Property(u => u.Password)
                .IsRequired();

            builder
                .Property(u => u.WalletBalance)
                .HasColumnType("decimal(18, 2)");

            builder
                .Property(u => u.IsMerchant)
                .IsRequired();
        }
    }
}
