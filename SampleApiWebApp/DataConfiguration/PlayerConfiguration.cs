﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp.DataConfiguration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.GivenName)
                .IsRequired()
                .HasMaxLength(Player.NameMaxLength);

            builder.Property(i => i.Surname)
                .IsRequired()
                .HasMaxLength(Player.NameMaxLength);

            builder.HasOne(i => i.Team)
                .WithMany(i => i.Players)
                .HasForeignKey(i => i.TeamId);

            builder.HasIndex(i => new { i.TeamId, i.SquadNumber })
                .IsUnique();
        }
    }
}
