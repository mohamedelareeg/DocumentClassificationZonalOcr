using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.MVC.Clients
{
    public class FormSampleClient : BaseClient, IFormSampleClient
    {
        public FormSampleClient(IHttpClientFactory httpClientFactory, ILogger<FormSampleClient> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
        }

        public async Task<CustomList<FormDto>> GetAllFormsAsync(DataTableOptionsDto options)
        {
            return await PostAsync<DataTableOptionsDto, CustomList<FormDto>>("api/Form/all", options);
        }

        public async Task<FormDto> GetFormByIdAsync(int formId)
        {
            return await GetAsync<FormDto>($"api/Form/get/{formId}");
        }

        public async Task<FormDto> CreateFormAsync(string name)
        {
            return await PostAsync<string, FormDto>("api/Form/create", name);
        }

        public async Task<bool> ModifyFormNameAsync(int formId, string newName)
        {
            return await PutAsync<string, bool>($"api/Form/modify/{formId}", newName);
        }

        public async Task<bool> RemoveFormAsync(int formId)
        {
            return await DeleteAsync<bool>($"api/Form/remove/{formId}");
        }
    }
}
