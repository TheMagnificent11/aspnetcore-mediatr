using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp.DataConfiguration
{
    /// <summary>
    /// Team Configuration
    /// </summary>
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        /// <summary>
        /// Configures tee team entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the team entity</param>
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(i => i.Name)
                .IsUnique();

            builder.HasMany(i => i.Players)
                .WithOne(i => i.Team)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(i => i.TeamId);
        }
    }
}
