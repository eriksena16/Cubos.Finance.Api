using Cubos.Finance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cubos.Finance.Data
{
    public class BankAccountMap : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Branch)
           .HasMaxLength(3)
           .IsUnicode(false);

            builder.Property(p => p.Account)
           .HasMaxLength(9)
           .IsUnicode(false);

            builder.HasIndex(p => p.Account)
             .IsUnique();

        }
    }
}
