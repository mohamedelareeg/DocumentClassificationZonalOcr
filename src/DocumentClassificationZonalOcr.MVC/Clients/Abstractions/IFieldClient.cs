using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.MVC.Clients.Abstractions
{
    public interface IFieldClient
    {
        Task<BaseResponse<FieldDto>> GetFieldByIdAsync(int fieldId);
        Task<BaseResponse<bool>> ModifyFieldAsync(int fieldId, FieldRequestDto fieldRequest);
        Task<BaseResponse<bool>> RemoveFieldAsync(int fieldId);
    }
}
