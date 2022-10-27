using System.Data.Entity.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularTestBackendServer.Data.Entities.Mappings;

public class StadiumEntityMap : EntityTypeConfiguration<StadiumEntity>
{
    public static void Configure(EntityTypeBuilder<StadiumEntity> builder)
    {
        builder.ToTable("Stadiums");
        builder.HasKey(e => e.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.City).IsRequired(false).HasMaxLength(200);
        builder.Property(p => p.Country).IsRequired(false).HasMaxLength(50);
        builder.Property(p => p.State).IsRequired(false).HasMaxLength(50);
        builder.Property(p => p.StadiumOpenYear).IsRequired(false);
        builder.Property(p => p.StadiumCloseYear).IsRequired(false);
        builder.Property(p => p.StadiumTypeId).IsRequired(false);
    }
}