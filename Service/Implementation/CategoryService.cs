using AutoMapper;
using CrudApi.Models.Domain;
using CrudApi.Models.DTOS;
using CrudApi.Repository.Interface;
using CrudApi.Service.Interface;

namespace CrudApi.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> AddAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            await _categoryRepository.AddAsync(category);

            return _mapper.Map<CategoryDto>(category);
        }

        public Task<bool> CategoryExists(Guid id)
        {
            return _categoryRepository.CategoryExist(id);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoriesDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return categoriesDtos;
        }

        public async Task<CategoryDto> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public async Task RemoveAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            await _categoryRepository.RemoveAsync(category);
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);

            _mapper.Map(categoryDto, category);

            await _categoryRepository.UpdateAsync(category);

        }
    }
}
