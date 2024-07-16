using AutoMapper;
using Store.Application.Features.Products.Commands.CreateProduct;
using Store.Application.Features.Products.Queries.GetProductsList;
using Store.Domain;

namespace Store.Application.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {

            CreateMap<Product, GetProductsListViewModel>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateProductCommand, Product>().ReverseMap();

            CreateMap<Product, CreateProductViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        

        }


    }
}
