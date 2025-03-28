﻿using AutoMapper;
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
            CreateMap<Book, BookResponseDto>();


            CreateMap<BookReservation, BookReservationResponseDto>().
                ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"));
            CreateMap<BookReservationRequestDto, BookReservation>()

                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => Guid.Parse("612A98BC-6A00-4191-B961-A2D43484E0A6")));


            CreateMap<Book, BookSearchResponseDto>()
                      .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name));
            CreateMap<BookSearchRequestDto, Book>();

        }
    }
}
                 
