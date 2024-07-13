using AutoMapper;
using 学生干部考评管理系统模型.DTO;
using 学生干部考评管理系统模型.Enity;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;

namespace 学生干部考评管理系统API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                ;

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Id)));

            CreateMap<User, CreateUserDto>();

            CreateMap<CreateUserDto, User>();

            CreateMap<Evaluation, EvaluationDto>()
            .ForMember(dest => dest.StudentCadreName, opt => opt.MapFrom(src => src.StudentCadreInfo!=null ? src.StudentCadreInfo.Fullname ?? string.Empty:string.Empty))
            .ForMember(dest => dest.EvaluatorName, opt => opt.MapFrom(src => src.User != null ? src.User.Fullname ?? string.Empty : string.Empty));
        }
    }
}
