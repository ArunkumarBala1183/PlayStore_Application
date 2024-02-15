using AutoMapper;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;

namespace Playstore.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<App, AppDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
