using Fleet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Data.EntityTypeConfigurations
{
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> transactionBuilder)
        {
            transactionBuilder.ToTable("Transactions");

            transactionBuilder.HasKey(x=>x.Id);

            transactionBuilder.Property(x => x.FromTransportationId).IsRequired();
            transactionBuilder.Property(x => x.ToTransportationId).IsRequired();
            transactionBuilder.Property(x => x.Type).IsRequired();
            transactionBuilder.Property(x => x.IssueDate).IsRequired();
        }
    }
}
