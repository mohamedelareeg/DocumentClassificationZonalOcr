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

        public async Task<BaseResponse<FieldDto>> GetFieldByIdAsync(int fieldId)
        {
            return await GetAsync<BaseResponse<FieldDto>>($"api/Field/{fieldId}");
        }

        public async Task<BaseResponse<bool>> ModifyFieldAsync(int fieldId, string newName)
        {
            return await PutAsync<string, BaseResponse<bool>>($"api/Field/{fieldId}", newName);
        }

        public async Task<BaseResponse<bool>> RemoveFieldAsync(int fieldId)
        {
            return await DeleteAsync<BaseResponse<bool>>($"api/Field/{fieldId}");
        }
    }
}
