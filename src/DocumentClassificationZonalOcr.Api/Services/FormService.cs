using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.MappingExtensions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Shared.Requests;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _formRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IFormSampleRepository _formSampleRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FormService(IFormRepository formRepository, IFieldRepository fieldRepository, IFormSampleRepository formSampleRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _formRepository = formRepository;
            _fieldRepository = fieldRepository;
            _formSampleRepository = formSampleRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
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

            var fieldCreationRequest = Field.Create(field.Name, field.Type, formId);
            if (fieldCreationRequest.IsFailure)
                return Result.Failure<bool>(fieldCreationRequest.Error);

            form.AddField(fieldCreationRequest.Value);
            return await _formRepository.UpdateAsync(form);
        }

        public async Task<Result<bool>> AddSampleToFormAsync(int formId, IFormFile file, List<ZoneRequestDto>? zones = null)
        {
            var formResult = await _formRepository.GetByIdAsync(formId);
            if (formResult.IsFailure)
                return Result.Failure<bool>(formResult.Error);

            var form = formResult.Value;

            var formDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "forms", formId.ToString());
            if (!Directory.Exists(formDirectory))
            {
                Directory.CreateDirectory(formDirectory);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(formDirectory, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imagePath = Path.Combine("forms", formId.ToString(), uniqueFileName);
            var formSample = FormSample.Create(formId, imagePath);
            if (formSample.IsFailure)
                return Result.Failure<bool>(formSample.Error);

            form.AddSample(formSample.Value);

            var updateResult = await _formRepository.UpdateAsync(form);
            if (updateResult.IsFailure)
                return Result.Failure<bool>(updateResult.Error);

            return Result.Success(true);
        }

        public async Task<Result<CustomList<FieldDto>>> GetAllFormFieldsAsync(int formId, DataTableOptionsDto options)
        {
            var formResult = await _formRepository.GetFormFieldByIdAsync(formId, options);
            if (formResult.IsFailure)
                return Result.Failure<CustomList<FieldDto>>(formResult.Error);
            var formDto = formResult.Value;

            return Result.Success(formDto);
        }

        public async Task<Result<CustomList<FormSampleDto>>> GetAllFormSamplesAsync(int formId, DataTableOptionsDto options)
        {
            var formResult = await _formRepository.GetFormSampleByIdAsync(formId, options);
            if (formResult.IsFailure)
                return Result.Failure<CustomList<FormSampleDto>>(formResult.Error);

            var formDto = formResult.Value;

            return Result.Success(formDto);
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

            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.Value}";

            var formSampleDto = result.Value.ToDto(baseUrl);
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
