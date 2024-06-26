using AutoMapper;
using CrudApi.Models.Domain;
using CrudApi.Models.DTOS;

namespace CrudApi.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
