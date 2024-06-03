using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.MVC.Clients
{
    public class FormSampleClient : BaseClient, IFormSampleClient
    {
        public FormSampleClient(IHttpClientFactory httpClientFactory, ILogger<FormSampleClient> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
        }

        public async Task<BaseResponse<bool>> ModifyFormSampleImageAsync(int formSampleId, IFormFile newImage)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StreamContent(newImage.OpenReadStream()), "newImage", newImage.FileName);

            return await PutAsync<MultipartFormDataContent, BaseResponse<bool>>($"api/FormSample/{formSampleId}", formData);
        }

        public async Task<BaseResponse<IEnumerable<ZoneDto>>> GetAllZonesAsync(int formSampleId)
        {
            return await GetAsync<BaseResponse<IEnumerable<ZoneDto>>>($"api/FormSample/{formSampleId}/zones");
        }

        public async Task<BaseResponse<bool>> AddZoneAsync(int formSampleId, ZoneRequestDto zone)
        {
            return await PostAsync<ZoneRequestDto, BaseResponse<bool>>($"api/FormSample/{formSampleId}/zones", zone);
        }

        public async Task<BaseResponse<FormSampleDto>> GetFormSampleByIdAsync(int formSampleId)
        {
            return await GetAsync<BaseResponse<FormSampleDto>>($"api/FormSample/{formSampleId}");
        }

        public async Task<BaseResponse<bool>> RemoveFormSampleAsync(int fieldId)
        {
            return await DeleteAsync<BaseResponse<bool>>($"api/FormSample/{fieldId}");
        }

        public async Task<BaseResponse<bool>> RemoveAllZonesAsync(int formSampleId)
        {
            return await DeleteAsync<BaseResponse<bool>>($"api/FormSample/{formSampleId}/zones");
        }
    }
}
