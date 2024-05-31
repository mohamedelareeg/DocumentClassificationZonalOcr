using DocumentClassificationZonalOcr.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentClassificationZonalOcr.Api.Data.Configurations
{
    internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable(TableNames.Documents);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

        }
    }
}
