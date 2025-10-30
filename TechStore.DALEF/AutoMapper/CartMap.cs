using AutoMapper;
using EF = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

namespace TechStore.DALEF.AutoMapper
{
    public class CartMap : Profile
    {
        public CartMap()
        {
            CreateMap<EF.Cart, DTO.Cart>();
            CreateMap<DTO.Cart, EF.Cart>();
        }
    }
}