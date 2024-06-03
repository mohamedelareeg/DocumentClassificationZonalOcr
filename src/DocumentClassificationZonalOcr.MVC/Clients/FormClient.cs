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

        public async Task<BaseResponse<bool>> AddFieldToFormAsync(int formId, FieldRequestDto field)
        {
            return await PostAsync<FieldRequestDto, BaseResponse<bool>>($"api/Form/{formId}/fields/add", field);
        }

        public async Task<BaseResponse<bool>> AddSampleToFormAsync(int formId, FormSampleRequestDto sample)
        {
            return await PostAsync<FormSampleRequestDto, BaseResponse<bool>>($"api/Form/{formId}/samples/add", sample);
        }

        public async Task<BaseResponse<bool>> CreateFieldAsync(int formId, string name, FieldType type)
        {
            var fieldRequest = new FieldRequestDto { Name = name, Type = type };
            return await PostAsync<FieldRequestDto, BaseResponse<bool>>($"api/Form/{formId}/fields/add", fieldRequest);
        }

        public async Task<BaseResponse<FormDto>> CreateFormAsync(string name)
        {
            return await PostAsync<string, BaseResponse<FormDto>>("api/Form/create", name);
        }

        public async Task<BaseResponse<bool>> CreateFormSampleAsync(int formId, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BaseResponse<bool>.CreateNullDataResponse("Invalid image");
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
            return response.IsSuccessStatusCode ? BaseResponse<bool>.Success(true) : BaseResponse<bool>.CreateNullDataResponse("Failed to create form sample");
        }

        public async Task<BaseResponse<CustomList<FieldDto>>> GetAllFormFieldsAsync(int formId, DataTableOptionsDto options)
        {
            return await PostAsync<DataTableOptionsDto, BaseResponse<CustomList<FieldDto>>>($"api/Form/{formId}/fields", options);
        }

        public async Task<BaseResponse<CustomList<FormSampleDto>>> GetAllFormSamplesAsync(int formId, DataTableOptionsDto options)
        {
            return await PostAsync<DataTableOptionsDto, BaseResponse<CustomList<FormSampleDto>>>($"api/Form/{formId}/samples", options);
        }

        public async Task<BaseResponse<FormDto>> GetFormByIdAsync(int formId)
        {
            return await GetAsync<BaseResponse<FormDto>>($"api/Form/get/{formId}");
        }

        public async Task<BaseResponse<CustomList<FormDto>>> GetAllFormsAsync(DataTableOptionsDto options)
        {
            return await PostAsync<DataTableOptionsDto, BaseResponse<CustomList<FormDto>>>("api/Form/all", options);
        }

        public async Task<BaseResponse<bool>> ModifyFormNameAsync(int formId, string newName)
        {
            return await PutAsync<string, BaseResponse<bool>>($"api/Form/modify/{formId}", newName);
        }

        public async Task<BaseResponse<bool>> RemoveFormAsync(int formId)
        {
            return await DeleteAsync<BaseResponse<bool>>($"api/Form/remove/{formId}");
        }
    }
}
