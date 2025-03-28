using AutoMapper;
using DataAccessLayer.Data;
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

            CreateMap<FacultyDto, Faculty>().ReverseMap();

            CreateMap<DesignationDto, Designation>().ReverseMap();
            CreateMap<BookRequestDto, Book>();
            CreateMap<Book, BookResponseDto>().ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Branch.CourseId));


            CreateMap<BookReservation,BookReservationResponseDto>().
                ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName));
            CreateMap<BookReservationRequestDto, BookReservation>();


            CreateMap<Book, BookSearchResponseDto>()
                      .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name));
            CreateMap<BookSearchRequestDto, Book>();

            CreateMap<CourseDto, BookRequestDto>().ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
                 
