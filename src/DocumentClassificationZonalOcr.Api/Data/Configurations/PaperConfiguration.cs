using DocumentClassificationZonalOcr.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Configurations
{
    internal sealed class PaperConfiguration : IEntityTypeConfiguration<Paper>
    {
        public void Configure(EntityTypeBuilder<Paper> builder)
        {
            builder.ToTable(TableNames.Papers);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FilePath)
                .IsRequired();

        }
    }
}
