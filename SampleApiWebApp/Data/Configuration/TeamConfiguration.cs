using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp.Data.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(Team.FieldMaxLenghts.Name);

            builder.HasIndex(i => i.Name)
                .IsUnique();

            builder.HasMany(i => i.Players)
                .WithOne(i => i.Team)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(i => i.TeamId);
        }
    }
}
