using AutoMapper;
using EF = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

namespace TechStore.DALEF.AutoMapper
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<EF.Product, DTO.Product>();
            CreateMap<DTO.Product, EF.Product>();
        }
    }
}