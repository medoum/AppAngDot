using CrudApi.Models.Domain;

namespace CrudApi.Repository.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task RemoveAsync(Product entity);

        Task<bool> SkuExistsAsync(string sku);
    }
}
