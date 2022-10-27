using System.Data.Entity.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularTestBackendServer.Data.Entities.Mappings;

public class TeamEntityMap : EntityTypeConfiguration<TeamEntity>
{
    public static void Configure(EntityTypeBuilder<TeamEntity> builder)
    {
        builder.ToTable("Teams");
        builder.HasKey(e => e.Id);
        
        builder.Property(p => p.ConferenceId).IsRequired();
        builder.Property(p => p.ShortName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Abbreviation).IsRequired().HasMaxLength(10);
        builder.Property(p => p.DivisionId).IsRequired(false);
        
        builder.HasMany(e => e.HomeGames)
               .WithOne(e => e.HomeTeam)
               .HasForeignKey(e => e.HomeTeamId);
        
        builder.HasMany(e => e.AwayGames)
               .WithOne(e => e.AwayTeam)
               .HasForeignKey(e => e.AwayTeamId);
        
        builder.HasOne(e => e.Division)
               .WithMany(e => e.Teams)
               .HasForeignKey(e => e.DivisionId)
               .IsRequired(false);
        
        builder.HasOne(e => e.Conference)
               .WithMany(e => e.Teams)
               .HasForeignKey(e => e.ConferenceId)
               .IsRequired();
    }
}