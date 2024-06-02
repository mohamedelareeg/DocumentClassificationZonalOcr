using DocumentClassificationZonalOcr.Shared.Dtos;

namespace DocumentClassificationZonalOcr.MVC.Clients.Abstractions
{
    public interface IFieldClient
    {
        Task<FieldDto> GetFieldByIdAsync(int fieldId);
        Task<bool> ModifyFieldAsync(int fieldId, string newName);
        Task<bool> RemoveFieldAsync(int fieldId);
    }
}
