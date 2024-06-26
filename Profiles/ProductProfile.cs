using AutoMapper;
using CrudApi.Models.Domain;
using CrudApi.Models.DTOS;

namespace CrudApi.Profiles
{

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
