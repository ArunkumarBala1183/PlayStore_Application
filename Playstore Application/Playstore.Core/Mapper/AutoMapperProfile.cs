// using AutoMapper;
// using Playstore.Contracts.Data.Entities;
// using Playstore.Contracts.DTO;

// namespace Playstore.Core.Mapper
// {
//     public class AutoMapperProfile : Profile
//     {
//         public AutoMapperProfile()
//         {
//             CreateMap<App, AppDTO>();
//             CreateMap<User, UserDTO>();
//             CreateMap<Users, AllUsersDTO>().ReverseMap();
//             CreateMap<AllUsersDTO, Users>().ReverseMap();
//             CreateMap<IEnumerable<Users>, IEnumerable<AllUsersDTO>>().ReverseMap();

//             CreateMap<Users , RegisterUsersDTO>().ReverseMap();
//             CreateMap<UserCredentials , RegisterUsersDTO>().ReverseMap();
//         }
//     }
// }
using AutoMapper;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using System;

namespace Playstore.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<App, AppDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Users, AllUsersDTO>().ReverseMap();
            // CreateMap<List<Users>,List<AllUsersDTO>>().ReverseMap();

            //CreateMap<AllUsersDTO, Users>()
                //.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => new DateTime(src.DateOfBirth.Year, src.DateOfBirth.Month, src.DateOfBirth.Day)));
                
            //CreateMap<IEnumerable<Users>, IEnumerable<AllUsersDTO>>().ReverseMap();

            CreateMap<Users, RegisterUsersDTO>().ReverseMap();
            CreateMap<UserCredentials, RegisterUsersDTO>().ReverseMap();
        }
    }
}
