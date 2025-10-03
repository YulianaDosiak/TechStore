using AutoMapper;
using EF = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

namespace TechStore.DALEF.AutoMapper
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<EF.Product, DTO.Product>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<DTO.Product, EF.Product>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
