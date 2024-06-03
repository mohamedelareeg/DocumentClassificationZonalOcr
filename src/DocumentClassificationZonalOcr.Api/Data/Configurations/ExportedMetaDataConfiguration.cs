using DocumentClassificationZonalOcr.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentClassificationZonalOcr.Api.Data.Configurations
{
    internal sealed class ExportedMetaDataConfiguration : IEntityTypeConfiguration<ExportedMetaData>
    {
        public void Configure(EntityTypeBuilder<ExportedMetaData> builder)
        {
            builder.ToTable(TableNames.ExportedMetaData);

            builder.HasKey(x => x.Id);


        }
    }
}
