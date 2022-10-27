using System.Data.Entity.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularTestBackendServer.Data.Entities.Mappings;

public class DivisionEntityMap : EntityTypeConfiguration<DivisionEntity>
{
    public static void Configure(EntityTypeBuilder<DivisionEntity> builder)
    {
        builder.ToTable("Divisions");
        builder.HasKey(e => e.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

        var divisions = new List<DivisionEntity>
        {
            new DivisionEntity
            {
                Id = 1,
                Name = "NFC West"
            },
            new DivisionEntity
            {
                Id = 2,
                Name = "NFC East"
            },
            new DivisionEntity
            {
                Id = 3,
                Name = "NFC South"
            },
            new DivisionEntity
            {
                Id = 4,
                Name = "NFC North"
            },
            new DivisionEntity
            {
                Id = 5,
                Name = "AFC West"
            },
            new DivisionEntity
            {
                Id = 6,
                Name = "AFC East"
            },
            new DivisionEntity
            {
                Id = 7,
                Name = "AFC South"
            },
            new DivisionEntity
            {
                Id = 8,
                Name = "AFC North"
            },
            new DivisionEntity
            {
                Id = 9,
                Name = "NFC Central"
            },
            new DivisionEntity
            {
                Id = 10,
                Name = "AFC Central"
            }
        };

        builder.HasData(divisions);
    }
}