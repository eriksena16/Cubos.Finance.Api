using Cubos.Finance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cubos.Finance.Data
{
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
