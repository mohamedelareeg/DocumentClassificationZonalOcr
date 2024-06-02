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

        public async Task<bool> ModifyFormSampleImageAsync(int formSampleId, IFormFile newImage)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StreamContent(newImage.OpenReadStream()), "newImage", newImage.FileName);

            return await PutAsync<MultipartFormDataContent, bool>($"api/FormSample/{formSampleId}", formData);
        }

        public async Task<IEnumerable<ZoneDto>> GetAllZonesAsync(int formSampleId)
        {
            return await GetAsync<IEnumerable<ZoneDto>>($"api/FormSample/{formSampleId}/zones");
        }

        public async Task<bool> AddZoneAsync(int formSampleId, ZoneRequestDto zone)
        {
            return await PostAsync<ZoneRequestDto, bool>($"api/FormSample/{formSampleId}/zones", zone);
        }

        public async Task<FormSampleDto> GetFormSampleByIdAsync(int formSampleId)
        {
            return await GetAsync<FormSampleDto>($"api/FormSample/{formSampleId}");
        }



        public async Task<bool> RemoveFormSampleAsync(int fieldId)
        {
            return await DeleteAsync<bool>($"api/FormSample/{fieldId}");
        }

    }
}
