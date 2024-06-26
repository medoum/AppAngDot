using CrudApi.Models.Domain;

namespace CrudApi.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task RemoveAsync(Category entity);

        Task<bool> CategoryExist(Guid id);
    }
}
