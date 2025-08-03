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
}
