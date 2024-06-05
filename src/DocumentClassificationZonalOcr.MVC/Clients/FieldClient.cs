using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

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

        public async Task<BaseResponse<bool>> ModifyFieldAsync(int fieldId, FieldRequestDto fieldRequest)
        {
            return await PutAsync<FieldRequestDto, BaseResponse<bool>>($"api/Field/{fieldId}", fieldRequest);
        }

        public async Task<BaseResponse<bool>> RemoveFieldAsync(int fieldId)
        {
            return await DeleteAsync<BaseResponse<bool>>($"api/Field/{fieldId}");
        }
    }
}
