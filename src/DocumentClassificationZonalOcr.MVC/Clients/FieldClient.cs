using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;

namespace DocumentClassificationZonalOcr.MVC.Clients
{
    public class FieldClient : BaseClient, IFieldClient
    {
        public FieldClient(IHttpClientFactory httpClientFactory, ILogger<FieldClient> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
        }

        public async Task<FieldDto> GetFieldByIdAsync(int fieldId)
        {
            return await GetAsync<FieldDto>($"api/Field/{fieldId}");
        }

        public async Task<bool> ModifyFieldAsync(int fieldId, string newName)
        {
            return await PutAsync<string, bool>($"api/Field/{fieldId}", newName);
        }

        public async Task<bool> RemoveFieldAsync(int fieldId)
        {
            return await DeleteAsync<bool>($"api/Field/{fieldId}");
        }
    }
}
