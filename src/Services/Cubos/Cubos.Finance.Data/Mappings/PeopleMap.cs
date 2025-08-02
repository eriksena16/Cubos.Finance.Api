using Cubos.Finance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cubos.Finance.Data
{
    public class PeopleMap : IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Document)
           .HasMaxLength(20)
           .IsUnicode(false);

            builder.Property(p => p.Password)
           .HasMaxLength(200)
           .IsUnicode(false);

            builder.HasIndex(p => p.Document)
             .IsUnique();

        }
    }
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
    public class CardMap : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Cvv)
           .HasMaxLength(3)
           .IsUnicode(false);

            builder.HasIndex(p => p.Number)
             .IsUnique();

            builder.HasOne(p => p.BankAccount)
            .WithMany(c => c.Cards)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
