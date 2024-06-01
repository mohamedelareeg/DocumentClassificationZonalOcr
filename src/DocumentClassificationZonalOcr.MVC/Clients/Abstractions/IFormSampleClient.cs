using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.MVC.Clients.Abstractions
{
    public interface IFormSampleClient
    {
        Task<CustomList<FormDto>> GetAllFormsAsync(DataTableOptionsDto options);
        Task<FormDto> GetFormByIdAsync(int formId);
        Task<FormDto> CreateFormAsync(string name);
        Task<bool> ModifyFormNameAsync(int formId, string newName);
        Task<bool> RemoveFormAsync(int formId);
    }
}
