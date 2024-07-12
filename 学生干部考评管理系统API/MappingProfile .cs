using AutoMapper;
using 学生干部考评管理系统模型.DTO;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;

namespace 学生干部考评管理系统API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Id)));

            CreateMap<User, CreateUserDto>();

            CreateMap<CreateUserDto, User>();
        }
    }
}
