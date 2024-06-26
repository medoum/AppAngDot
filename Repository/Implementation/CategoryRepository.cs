using CrudApi.Data;
using CrudApi.Models.Domain;
using CrudApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;
        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Category entity)
        {
            await _dbContext.Categories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CategoryExist(Guid id)
        {
            return await _dbContext.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
        }

        public async Task RemoveAsync(Category entity)
        {
            _dbContext.Categories.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            _dbContext.Categories.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
