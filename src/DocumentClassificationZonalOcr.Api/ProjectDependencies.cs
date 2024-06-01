using DocumentClassificationZonalOcr.Api.Data;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DocumentClassificationZonalOcr.Api.Data.Repositories;
using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;

namespace DocumentClassificationZonalOcr.Api
{
    public static class ProjectDependencies
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string connectionString = configuration.GetConnectionString("SqlServer");
                options.UseSqlServer(connectionString);
            });

            //Repositories
            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IFormSampleRepository, FormSampleRepository>();
            services.AddScoped<IZoneRepository, ZoneRepository>();
            services.AddScoped<IFieldRepository, FieldRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IPaperRepository, PaperRepository>();
            services.AddScoped<IExportedMetaDataRepository, ExportedMetaDataRepository>();
            services.AddScoped<IFormDetectionSettingsRepository, FormDetectionSettingsRepository>();

            //Services
            services.AddScoped<IFormSampleService, FormSampleService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IZoneService, ZoneService>();
            services.AddScoped<IFieldService, FieldService>();
            services.AddScoped<IPaperService, PaperService>();
            services.AddScoped<IImageEnhancementService, ImageEnhancementService>();
            services.AddScoped<IFormDetectionSettingService, FormDetectionSettingService>();
            services.AddScoped<IFormDetectionService, FormDetectionService>();

            return services;
        }
    }
}
