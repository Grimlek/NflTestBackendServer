using System.Data.Entity.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularTestBackendServer.Data.Entities.Mappings;

public class ConferenceEntityMap : EntityTypeConfiguration<ConferenceEntity>
{
    public static void Configure(EntityTypeBuilder<ConferenceEntity> builder)
    {
        builder.ToTable("Conferences");
        builder.HasKey(e => e.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

        var conferences = new List<ConferenceEntity>
        {
            new ConferenceEntity
            {
                Id = 1,
                Name = "NFC"
            },
            new ConferenceEntity
            {
                Id = 2,
                Name = "AFC"
            }
        };

        builder.HasData(conferences);
    }
}