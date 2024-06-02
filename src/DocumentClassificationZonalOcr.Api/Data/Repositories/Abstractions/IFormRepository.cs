using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Results;

public interface IFormRepository
{
    Task<Result<Form>> GetByIdAsync(int id);
    Task<Result<Form>> CreateAsync(Form form);
    Task<Result<bool>> UpdateAsync(Form form);
    Task<Result<bool>> DeleteAsync(int id);
    Task<Result<List<Zone>>> GetAllFormZonesAsync(int formId);
    Task<Result<CustomList<FormDto>>> GetAllFormsAsync(DataTableOptionsDto options);
    Task<Result<CustomList<FieldDto>>> GetFormFieldByIdAsync(int formId, DataTableOptionsDto options);
    Task<Result<CustomList<FormSampleDto>>> GetFormSampleByIdAsync(int formId, DataTableOptionsDto options);


}