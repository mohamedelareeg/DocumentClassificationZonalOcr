using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DocumentClassificationZonalOcr.Api.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        }
    }
}
