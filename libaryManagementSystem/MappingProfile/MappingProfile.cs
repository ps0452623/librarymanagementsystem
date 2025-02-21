using AutoMapper;
using DataAcessLayer;
using DTO;


namespace LibaryManagementSystem.MappingProfile
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            
            CreateMap<RegistrationDto, ApplicationUser>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap();
                 
        }
    }
}