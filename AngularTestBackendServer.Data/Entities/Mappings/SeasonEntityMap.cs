using System.Data.Entity.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularTestBackendServer.Data.Entities.Mappings;

public class SeasonEntityMap : EntityTypeConfiguration<SeasonEntity>
{
    public static void Configure(EntityTypeBuilder<SeasonEntity> builder)
    {
        builder.ToTable("Seasons");
        builder.HasKey(e => e.Id);

        builder.Property(p => p.Year).IsRequired();

        builder.HasMany(e => e.WeeklySchedules)
               .WithOne(e => e.Season)
               .HasForeignKey(e => e.SeasonId);
    }
}