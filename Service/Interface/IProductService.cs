using CrudApi.Models.DTOS;

namespace CrudApi.Service.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<ProductDto> AddAsync(ProductDto productDto);
        Task UpdateAsync(ProductDto productDto);
        Task RemoveAsync(Guid id);
        Task<bool> SkuExistsAsync(string sku);
    }
}
