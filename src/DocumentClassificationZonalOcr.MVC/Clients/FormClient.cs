using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Shared.Requests;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.MVC.Clients
{
    public class FormClient : BaseClient, IFormClient
    {
        public FormClient(IHttpClientFactory httpClientFactory, ILogger<FormClient> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
        }

        public async Task<bool> AddFieldToFormAsync(int formId, FieldRequestDto field)
        {
            return await PostAsync<FieldRequestDto, bool>($"api/Form/{formId}/fields/add", field);
        }

        public async Task<bool> AddSampleToFormAsync(int formId, FormSampleRequestDto sample)
        {
            return await PostAsync<FormSampleRequestDto, bool>($"api/Form/{formId}/samples/add", sample);
        }

        public async Task<FieldDto> CreateFieldAsync(int formId, string name, FieldType type)
        {
            var fieldRequest = new FieldRequestDto { Name = name, Type = type };
            return await PostAsync<FieldRequestDto, FieldDto>($"api/Form/{formId}/fields/create", fieldRequest);
        }

        public async Task<FormDto> CreateFormAsync(string name)
        {
            return await PostAsync<string, FormDto>("api/Form/create", name);
        }

        public async Task<FormSampleDto> CreateFormSampleAsync(int formId, IFormFile image)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StreamContent(image.OpenReadStream()), "image", image.FileName);

            return await PostAsync<MultipartFormDataContent, FormSampleDto>($"api/Form/{formId}/samples/create", formData);
        }

        public async Task<IEnumerable<FieldDto>> GetAllFormFieldsAsync(int formId)
        {
            return await GetAsync<IEnumerable<FieldDto>>($"api/Form/{formId}/fields");
        }

        public async Task<IEnumerable<FormSampleDto>> GetAllFormSamplesAsync(int formId)
        {
            return await GetAsync<IEnumerable<FormSampleDto>>($"api/Form/{formId}/samples");
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
