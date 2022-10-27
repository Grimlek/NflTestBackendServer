using System.Data.Entity.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularTestBackendServer.Data.Entities.Mappings;

public class ScheduleEntityMap : EntityTypeConfiguration<ScheduleEntity>
{
    public static void Configure(EntityTypeBuilder<ScheduleEntity> builder)
    {
        builder.ToTable("Schedules");
        builder.HasKey(e => e.Id);
        
        builder.Property(p => p.Week).IsRequired().HasMaxLength(50);
        builder.Property(p => p.SeasonId).IsRequired();
        builder.Property(p => p.IsPlayoffGame).HasDefaultValue(false);
        
        builder.HasMany(e => e.WeeklyGames)
               .WithOne(e => e.Schedule)
               .HasForeignKey(e => e.ScheduleId)
               .IsRequired();

        builder.HasOne(e => e.Season)
               .WithMany(e => e.WeeklySchedules)
               .HasForeignKey(e => e.SeasonId)
               .IsRequired();
    }
}