using AutoMapper;
using HumanResources.Application.Authentication;
using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.UserModelDto;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace HumanResources.Application.AutoMapperProfile
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {

            CreateMap<UserInfo, GetInfromationsDto>()
                .ForMember(pr => pr.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(pr => pr.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(pr => pr.Phone, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(pr => pr.UserCode, opt=>opt.MapFrom(src=>src.User.UserCode))
                .ForMember(pr => pr.EducationLevelName, opt=>opt.MapFrom(src=>src.EducationTitle.ToString()));


            CreateMap<Arrivals, GetArrivalsDto>();

            CreateMap<Absence, AbsenceInfoDto>()
                .ForMember(pr=>pr.AbsenceTypeName, opt=>opt.MapFrom(src=>src.AbsencesType.Name));

        }
    }
}
