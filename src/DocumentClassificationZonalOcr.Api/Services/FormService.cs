using DocumentClassificationZonalOcr.Api.Data.Repositories;
using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using DocumentClassificationZonalOcr.Shared.Requests;
using DocumentClassificationZonalOcr.Api.MappingExtensions;
using Microsoft.AspNetCore.Mvc;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _formRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IFormSampleRepository _formSampleRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FormService(IFormRepository formRepository, IFieldRepository fieldRepository, IFormSampleRepository formSampleRepository, IWebHostEnvironment webHostEnvironment)
        {
            _formRepository = formRepository;
            _fieldRepository = fieldRepository;
            _formSampleRepository = formSampleRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Result<FormDto>> CreateFormAsync(string name)
        {
            var createFormResult = Form.Create(name);
            if (createFormResult.IsFailure)
                return Result.Failure<FormDto>(createFormResult.Error);

            var createResult = await _formRepository.CreateAsync(createFormResult.Value);
            if (createResult.IsFailure)
                return Result.Failure<FormDto>(createResult.Error);

            var formDto = createResult.Value.ToDto();
            return Result.Success(formDto);

        }

        public async Task<Result<bool>> ModifyFormNameAsync(int formId, string newName)
        {
            var formResult = await _formRepository.GetByIdAsync(formId);
            if (formResult.IsFailure)
                return Result.Failure<bool>(formResult.Error);

            var form = formResult.Value;
            form.ModifyName(newName);
            return await _formRepository.UpdateAsync(form);
        }

        public async Task<Result<bool>> RemoveFormAsync(int formId)
        {
            var result = await _formRepository.DeleteAsync(formId);
            if (result.IsFailure)
                return Result.Failure<bool>(result.Error);

            return Result.Success(true);
        }

        public async Task<Result<bool>> AddFieldToFormAsync(int formId, FieldRequestDto field)
        {
            var formResult = await _formRepository.GetByIdAsync(formId);
            if (formResult.IsFailure)
                return Result.Failure<bool>(formResult.Error);

            var form = formResult.Value;

            var fieldCreationRequest = Field.Create(field.Name,field.Type,formId);
            if(fieldCreationRequest.IsFailure)
                return Result.Failure<bool>(fieldCreationRequest.Error);

            form.AddField(fieldCreationRequest.Value);
            return await _formRepository.UpdateAsync(form);
        }

        public async Task<Result<bool>> AddSampleToFormAsync(int formId, FormSampleRequestDto sample)
        {
            var formResult = await _formRepository.GetByIdAsync(formId);
            if (formResult.IsFailure)
                return Result.Failure<bool>(formResult.Error);

            var form = formResult.Value;

            var formSample = sample.ToEntity(form.Id);
            form.AddSample(formSample);
            return await _formRepository.UpdateAsync(form);
        }

        public async Task<Result<IEnumerable<FieldDto>>> GetAllFormFieldsAsync(int formId)
        {
            var formResult = await _formRepository.GetByIdAsync(formId);
            if (formResult.IsFailure)
                return Result.Failure<IEnumerable<FieldDto>>(formResult.Error);
            var formDto = formResult.Value.ToDto();

            return Result.Success(formDto.Fields.AsEnumerable());
        }

        public async Task<Result<IEnumerable<FormSampleDto>>> GetAllFormSamplesAsync(int formId)
        {
            var formResult = await _formRepository.GetByIdAsync(formId);
            if (formResult.IsFailure)
                return Result.Failure<IEnumerable<FormSampleDto>>(formResult.Error);

            var formDto = formResult.Value.ToDto();

            return Result.Success(formDto.Samples.AsEnumerable());
        }

        public async Task<Result<FormDto>> GetFormByIdAsync(int formId)
        {
            var result = await _formRepository.GetByIdAsync(formId);
            if (result.IsFailure)
                return Result.Failure<FormDto>(result.Error);

            var formDto = result.Value.ToDto();

            return formDto;
        }
        public async Task<Result<FieldDto>> CreateFieldAsync(string name, FieldType type, int formId)
        {
            var result = Field.Create(name, type, formId);
            if (result.IsFailure)
                return Result.Failure<FieldDto>(result.Error);
            var createResult = await _fieldRepository.CreateAsync(result.Value);
            if (createResult.IsFailure)
                return Result.Failure<FieldDto>(createResult.Error);

            var fieldDto = createResult.Value.ToDto();
            return Result.Success(fieldDto);

        }
        public async Task<Result<FormSampleDto>> CreateFormSampleAsync(int formId, IFormFile image)
        {
            if (image == null || image.Length == 0)
                return Result.Failure<FormSampleDto>("CreateFormSampleAsync", "Image is required.");

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "forms", formId.ToString());
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            var createFormSampleResult = FormSample.Create(formId, filePath);
            if (createFormSampleResult.IsFailure)
                return Result.Failure<FormSampleDto>(createFormSampleResult.Error);

            var result = await _formSampleRepository.CreateAsync(createFormSampleResult.Value);
            if (result.IsFailure)
                return Result.Failure<FormSampleDto>(result.Error);

            var formSampleDto = result.Value.ToDto();
            return Result.Success(formSampleDto);
        }

        public async Task<Result<CustomList<FormDto>>> GetAllFormsAsync(DataTableOptionsDto options)
        {
            var result = await _formRepository.GetAllFormsAsync(options);
            if (result.IsFailure)
                return Result.Failure<CustomList<FormDto>>(result.Error);
            return Result.Success(result.Value);
        }
    }
}
