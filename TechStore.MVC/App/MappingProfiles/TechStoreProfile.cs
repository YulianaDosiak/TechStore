using AutoMapper;
using TechStore.DTO;
using TechStore.MVC.Models;
using TechStore.DALEF.Models;

namespace TechStore.MVC.App.MappingProfiles
{
    public class TechStoreProfile : Profile
    {
        public TechStoreProfile()
        {
            CreateMap<TechStore.DTO.Category, CategoryViewModel>().ReverseMap();

            CreateMap<TechStore.DALEF.Models.Category, TechStore.DTO.Category>().ReverseMap();

            CreateMap<TechStore.DALEF.Models.User, TechStore.DTO.User>()
               .ForMember(d => d.UserID, o => o.MapFrom(s => s.UserId))
               .ReverseMap()
               .ForMember(d => d.UserId, o => o.MapFrom(s => s.UserID));

            CreateMap<TechStore.DALEF.Models.Product, TechStore.DTO.Product>()
               .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductId))
               .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryId ?? 0))
               .ReverseMap();

            CreateMap<TechStore.DTO.Product, ProductViewModel>().ReverseMap();
        }
    }
}