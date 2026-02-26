using Assignment_1.DTOs;
using Assignment_1.Models;
using AutoMapper;

namespace Assignment_1.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDTO>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();
        }
    }
}
