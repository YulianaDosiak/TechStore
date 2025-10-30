using AutoMapper;
using EF = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

namespace TechStore.DALEF.AutoMapper
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<EF.Category, DTO.Category>();
            CreateMap<DTO.Category, EF.Category>();
        }
    }
}