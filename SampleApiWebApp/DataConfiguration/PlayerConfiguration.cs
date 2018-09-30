using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp.DataConfiguration
{
    /// <summary>
    /// Player Configuration
    /// </summary>
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        /// <summary>
        /// Configures the player entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the player entity</param>
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.GivenName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Surname)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(i => i.Team)
                .WithMany(i => i.Players)
                .HasForeignKey(i => i.TeamId);

            builder.HasIndex(i => new { i.TeamId, i.SquadNumber })
                .IsUnique();
        }
    }
}
