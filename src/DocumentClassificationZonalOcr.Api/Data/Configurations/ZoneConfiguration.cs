using DocumentClassificationZonalOcr.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Configurations
{
    internal sealed class ZoneConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.ToTable(TableNames.Zones);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.X)
                .IsRequired();

            builder.Property(x => x.Y)
                .IsRequired();

            builder.Property(x => x.ActualWidth)
                .IsRequired();

            builder.Property(x => x.ActualHeight)
                .IsRequired();

            builder.Property(x => x.ActualImageWidth)
                .IsRequired();

            builder.Property(x => x.ActualImageHeight)
                .IsRequired();

            builder.Property(x => x.FormSampleId)
                .IsRequired();

        }
    }
}
