using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

public interface IFormRepository
{
    Task<Result<Form>> GetByIdAsync(int id);
    Task<Result<Form>> CreateAsync(Form form);
    Task<Result<bool>> UpdateAsync(Form form);
    Task<Result<bool>> DeleteAsync(int id);
}