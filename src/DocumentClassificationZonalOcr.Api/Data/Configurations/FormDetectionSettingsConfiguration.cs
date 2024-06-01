using DocumentClassificationZonalOcr.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Configurations
{
    public class FormDetectionSettingsConfiguration : IEntityTypeConfiguration<FormDetectionSetting>
    {
        public void Configure(EntityTypeBuilder<FormDetectionSetting> builder)
        {
            builder.ToTable(TableNames.FormDetectionSettings);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FormSimilarity)
                  .HasColumnType("decimal(18,2)");

        }
    }
}
