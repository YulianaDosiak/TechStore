using AutoMapper;
using EF = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

namespace TechStore.DALEF.AutoMapper
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<EF.User, DTO.User>();
            CreateMap<DTO.User, EF.User>();
        }
    }
}