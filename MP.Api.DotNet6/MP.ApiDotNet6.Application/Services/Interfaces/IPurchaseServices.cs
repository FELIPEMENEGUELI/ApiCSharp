using MP.ApiDotNet6.Application.DTOs;

namespace MP.ApiDotNet6.Application.Services.Interfaces
{
    public  interface IPurchaseServices
    {
        Task<ResultServices<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO);
        Task<ResultServices<PurchaseDetailDTO>> GetByIdAsync(int id);
        Task<ResultServices<ICollection<PurchaseDetailDTO>>> GetAsync();
        Task<ResultServices<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO);
        Task<ResultServices> RemoveAsync(int id);
    }
}
