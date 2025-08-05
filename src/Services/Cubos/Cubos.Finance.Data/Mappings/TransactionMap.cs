using Cubos.Finance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cubos.Finance.Data
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(p => p.BankAccount)
            .WithMany(c => c.Transactions)
            .HasForeignKey(p => p.BankAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
