using DocumentClassificationZonalOcr.Api.Data.Repositories;
using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Dtos;
using DocumentClassificationZonalOcr.Api.Enums;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;

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

        public async Task<Result<Form>> CreateFormAsync(string name)
        {
            var createFormResult = Form.Create(name);
            if (createFormResult.IsFailure)
                return Result.Failure<Form>(createFormResult.Error);
            return await _formRepository.CreateAsync(createFormResult.Value);
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

        public async Task<Result<bool>> AddFieldToFormAsync(int formId, Field field)
        {
            var formResult = await _formRepository.GetByIdAsync(formId);
            if (formResult.IsFailure)
                return Result.Failure<bool>(formResult.Error);

            var form = formResult.Value;
            form.AddField(field);
            return await _formRepository.UpdateAsync(form);
        }

        public async Task<Result<bool>> AddSampleToFormAsync(int formId, FormSample sample)
        {
            var formResult = await _formRepository.GetByIdAsync(formId);
            if (formResult.IsFailure)
                return Result.Failure<bool>(formResult.Error);

            var form = formResult.Value;
            form.AddSample(sample);
            return await _formRepository.UpdateAsync(form);
        }

        public async Task<Result<IEnumerable<Field>>> GetAllFormFieldsAsync(int formId)
        {
            var formResult = await _formRepository.GetByIdAsync(formId);
            if (formResult.IsFailure)
                return Result.Failure<IEnumerable<Field>>(formResult.Error);

            var form = formResult.Value;
            return Result.Success(form.Fields.AsEnumerable());
        }

        public async Task<Result<IEnumerable<FormSample>>> GetAllFormSamplesAsync(int formId)
        {
            var formResult = await _formRepository.GetByIdAsync(formId);
            if (formResult.IsFailure)
                return Result.Failure<IEnumerable<FormSample>>(formResult.Error);

            var form = formResult.Value;
            return Result.Success(form.Samples.AsEnumerable());
        }

        public async Task<Result<Form>> GetFormByIdAsync(int formId)
        {
            var result = await _formRepository.GetByIdAsync(formId);
            if (result.IsFailure)
                return Result.Failure<Form>(result.Error);

            return result.Value;
        }
        public async Task<Result<Field>> CreateFieldAsync(string name, FieldType type, int formId)
        {
            var result = Field.Create(name, type, formId);
            if (result.IsFailure)
                return Result.Failure<Field>(result.Error);

            return await _fieldRepository.CreateAsync(result.Value);
        }
        public async Task<Result<FormSample>> CreateFormSampleAsync(int formId, IFormFile image)
        {
            if (image == null || image.Length == 0)
                return Result.Failure<FormSample>("CreateFormSampleAsync", "Image is required.");

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
                return Result.Failure<FormSample>(createFormSampleResult.Error);

            var result = await _formSampleRepository.CreateAsync(createFormSampleResult.Value);
            if (result.IsFailure)
                return Result.Failure<FormSample>(result.Error);

            return Result.Success(result.Value);
        }
    }
}
