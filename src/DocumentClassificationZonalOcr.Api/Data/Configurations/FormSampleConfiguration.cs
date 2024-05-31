using DocumentClassificationZonalOcr.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Configurations
{
    internal sealed class FormSampleConfiguration : IEntityTypeConfiguration<FormSample>
    {
        public void Configure(EntityTypeBuilder<FormSample> builder)
        {
            builder.ToTable(TableNames.FormSamples);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImagePath)
                .IsRequired();

        }
    }
}
