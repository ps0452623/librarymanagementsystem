using AutoMapper;
using DataAcessLayer;
using DataAcessLayer.Entities;
using DTO;


namespace LibaryManagementSystem.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Branch, BranchDto>().ReverseMap();

           

            CreateMap<RegistrationDto, ApplicationUser>()
                     .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap();
        }

    }  
}
