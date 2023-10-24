using DesafioTransferencia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioTransferencia.Data.Map
{
    public class TransferMap : IEntityTypeConfiguration<TransactionModel>
    {
        public void Configure(EntityTypeBuilder<TransactionModel> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Id)
                .HasColumnName("Id");

            builder
                .Property(t => t.Value)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder
                .Property(t => t.PayerId)
                .IsRequired();

            builder
                .Property(t => t.PayeeId)
                .IsRequired();

            builder
                .Property(t => t.TransferDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(t => t.Status)
                .IsRequired();
        }
    }
}