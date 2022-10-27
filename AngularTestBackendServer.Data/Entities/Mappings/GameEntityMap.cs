using System.Data.Entity.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularTestBackendServer.Data.Entities.Mappings;

public class GameEntityMap : EntityTypeConfiguration<GameEntity>
{
    public static void Configure(EntityTypeBuilder<GameEntity> builder)
    {
        builder.ToTable("Games");
        builder.HasKey(e => e.Id);

        builder.Property(p => p.ScheduleId).IsRequired();
        builder.Property(p => p.StadiumId).IsRequired();
        builder.Property(p => p.AwayTeamId).IsRequired();
        builder.Property(p => p.HomeTeamId).IsRequired();
        builder.Property(p => p.AwayTeamScore).IsRequired(false);
        builder.Property(p => p.HomeTeamScore).IsRequired(false);
        builder.Property(p => p.WasStadiumNeutral).IsRequired().HasDefaultValue(false);
        builder.Property(p => p.WeatherWindMph).IsRequired(false);
        builder.Property(p => p.WeatherHumidity).IsRequired(false);
        builder.Property(p => p.WeatherTemperature).IsRequired(false);
        builder.Property(p => p.GameDate).IsRequired();
        
        builder.HasOne(e => e.HomeTeam)
               .WithMany(e => e.HomeGames)
               .HasForeignKey(e => e.HomeTeamId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(e => e.AwayTeam)
               .WithMany(e => e.AwayGames)
               .HasForeignKey(e => e.AwayTeamId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(e => e.Schedule)
               .WithMany(e => e.WeeklyGames)
               .HasForeignKey(e => e.ScheduleId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(e => e.Stadium)
               .WithMany(e => e.Games)
               .HasForeignKey(e => e.StadiumId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}