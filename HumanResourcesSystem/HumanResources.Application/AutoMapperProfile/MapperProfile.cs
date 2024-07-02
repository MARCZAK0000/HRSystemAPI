using AutoMapper;
using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.DepartmentModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.UserModelDto;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Net;

namespace HumanResources.Application.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<UserInfo, GetInfromationsDto>()
                .ForMember(pr => pr.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(pr => pr.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(pr => pr.Phone, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(pr => pr.UserCode, opt => opt.MapFrom(src => src.User.UserCode))
                .ForMember(pr => pr.EducationLevelName, opt => opt.MapFrom(src => src.EducationTitle.ToString()))
                .ForMember(pr => pr.IsEmailConfirmed, opt => opt.MapFrom(src => src.User.EmailConfirmed));


            CreateMap<Attendance, GetArrivalsDto>();

            CreateMap<Absence, AbsenceInfoDto>()
                .ForMember(pr => pr.AbsenceTypeName, opt => opt.MapFrom(src => src.AbsencesType.Name));

            CreateMap<Absence, AbsenceDecisionDto>()
                .ForMember(pr => pr.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(pr => pr.Surname, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(pr => pr.UserCode, opt => opt.MapFrom(src => src.User.UserCode))
                .ForMember(pr => pr.AbsenseTitle, opt => opt.MapFrom(src => src.Name))
                .ForMember(pr => pr.AbsenceName, opt => opt.MapFrom(src => src.AbsencesType.Name))
                .ForMember(pr => pr.StartTime, opt => opt.MapFrom(src => src.StartTime.Date))
                .ForMember(pr => pr.EndTime, opt => opt.MapFrom(src => src.EndTime.Date));


            CreateMap<Departments, DepartmentInfoDto>()
                .ForMember(pr => pr.DeparmentID, opt => opt.MapFrom(src => src.Id));

            CreateMap<UserInfo, DepartmentUserInfoDto>()
                .ForMember(pr => pr.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(pr => pr.Phone, opt => opt.MapFrom(src => src.User.PhoneNumber));


            //CreateMap<Departments, DeparmentEmployee>()
            //    .ForMember(pr => pr.DeparmentID, opt => opt.MapFrom(src => src.Id));
                
            //CreateMap<UserInfo, DeparmentEmployeeInfo>();
                

        }
    }
}
