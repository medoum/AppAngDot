using CrudApi.Models.Domain;
using CrudApi.Models.DTOS;

namespace CrudApi.Service.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(Guid id);
        Task<CategoryDto> AddAsync(CategoryDto categoryDto);
        Task UpdateAsync(CategoryDto categoryDto);
        Task RemoveAsync(Guid id);
        Task<bool> CategoryExists(Guid id);
    }
}
