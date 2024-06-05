using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;

namespace DocumentClassificationZonalOcr.MVC.Clients
{
    public class FormDetectionSettingClient : BaseClient, IFormDetectionSettingClient
    {
        public FormDetectionSettingClient(IHttpClientFactory httpClientFactory, ILogger<FormDetectionSettingClient> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
        }

        public async Task<BaseResponse<FormDetectionSettingDto>> GetSettingsAsync()
        {
            return await GetAsync<BaseResponse<FormDetectionSettingDto>>("api/FormDetectionSettings/get");
        }

        public async Task<BaseResponse<bool>> UpdateSettingsAsync(FormDetectionSettingDto settings)
        {
            return await PostAsync<FormDetectionSettingDto, BaseResponse<bool>>("api/FormDetectionSettings/update", settings);
        }
    }
}
