using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp.Data.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.GivenName)
                .IsRequired()
                .HasMaxLength(Player.FieldNameMaxLengths.Name);

            builder.Property(i => i.Surname)
                .IsRequired()
                .HasMaxLength(Player.FieldNameMaxLengths.Name);

            builder.HasOne(i => i.Team)
                .WithMany(i => i.Players)
                .HasForeignKey(i => i.TeamId);

            builder.HasIndex(i => new { i.TeamId, i.Number })
                .IsUnique();
        }
    }
}
