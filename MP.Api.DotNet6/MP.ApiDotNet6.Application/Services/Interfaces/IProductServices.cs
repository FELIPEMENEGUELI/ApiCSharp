using MP.ApiDotNet6.Application.DTOs;

namespace MP.ApiDotNet6.Application.Services.Interfaces
{
    public interface IProductServices
    {
        Task<ResultServices<ProductDTO>> CreateAsync(ProductDTO productDTO);
        Task<ResultServices<ICollection<ProductDTO>>> GetAsync();
        Task<ResultServices<ProductDTO>> GetByIdAsync(int id);
        Task<ResultServices> UpdateAsync(ProductDTO productDTO);
        Task<ResultServices> DeleteAsync(int id);
        
    }
}
 