using System.Data.Entity.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularTestBackendServer.Data.Entities.Mappings;

public class StadiumTypeEntityMap : EntityTypeConfiguration<StadiumTypeEntity>
{
    public static void Configure(EntityTypeBuilder<StadiumTypeEntity> builder)
    {
        builder.ToTable("StadiumTypes");
        builder.HasKey(e => e.Id);
        
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        
        var stadiumTypes = new List<StadiumTypeEntity>
        {
            new StadiumTypeEntity
            {
               Id = -1,
               Name = "Unknown"
            },
            new StadiumTypeEntity
            {
                Id = 1,
                Name = "Outdoor"
            },
            new StadiumTypeEntity
            {
                Id = 2,
                Name = "Indoor"
            },
            new StadiumTypeEntity
            {
                Id = 3,
                Name = "Retractable"
            }
        };

        builder.HasData(stadiumTypes);
    }
}