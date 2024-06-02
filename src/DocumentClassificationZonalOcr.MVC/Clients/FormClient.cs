using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Shared.Requests;
using DocumentClassificationZonalOcr.Shared.Results;
using System.Net.Http.Headers;

namespace DocumentClassificationZonalOcr.MVC.Clients
{
    public class FormClient : BaseClient, IFormClient
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public FormClient(IHttpClientFactory httpClientFactory, ILogger<FormClient> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> AddFieldToFormAsync(int formId, FieldRequestDto field)
        {
            return await PostAsync<FieldRequestDto, bool>($"api/Form/{formId}/fields/add", field);
        }

        public async Task<bool> AddSampleToFormAsync(int formId, FormSampleRequestDto sample)
        {
            return await PostAsync<FormSampleRequestDto, bool>($"api/Form/{formId}/samples/add", sample);
        }

        public async Task<bool> CreateFieldAsync(int formId, string name, FieldType type)
        {
            var fieldRequest = new FieldRequestDto { Name = name, Type = type };
            return await PostAsync<FieldRequestDto, bool>($"api/Form/{formId}/fields/add", fieldRequest);
        }

        public async Task<FormDto> CreateFormAsync(string name)
        {
            return await PostAsync<string, FormDto>("api/Form/create", name);
        }

        public async Task<bool> CreateFormSampleAsync(int formId, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return false;
            }

            var uploadUrl = $"api/Form/{formId}/samples/add";
            using var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

            using var stream = image.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);

            using var formData = new MultipartFormDataContent();
            formData.Add(fileContent, "file", image.FileName);

            var response = await client.PostAsync(uploadUrl, formData);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<CustomList<FieldDto>> GetAllFormFieldsAsync(int formId, DataTableOptionsDto options)
        {
            return await PostAsync<DataTableOptionsDto, CustomList<FieldDto>>($"api/Form/{formId}/fields", options);
        }

        public async Task<CustomList<FormSampleDto>> GetAllFormSamplesAsync(int formId, DataTableOptionsDto options)
        {
            return await PostAsync<DataTableOptionsDto, CustomList<FormSampleDto>>($"api/Form/{formId}/samples", options);
        }

        public async Task<FormDto> GetFormByIdAsync(int formId)
        {
            return await GetAsync<FormDto>($"api/Form/get/{formId}");
        }
        public async Task<CustomList<FormDto>> GetAllFormsAsync(DataTableOptionsDto options)
        {
            return await PostAsync<DataTableOptionsDto, CustomList<FormDto>>("api/Form/all", options);
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
