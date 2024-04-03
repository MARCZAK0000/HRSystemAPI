using AutoMapper;
using HumanResources.Application.AutoMapperProfile;
using HumanResources.Domain.AttendanceModelDto;
using HumanResources.Domain.Repository;
using HumanResources.Domain.UserModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_Attendance.Handler
{
    public class AttendanceHandlerService : IAttendanceHandlerService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        private readonly IMapper _mapperProfile;

        public AttendanceHandlerService(IAttendanceRepository attendanceRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _mapperProfile = mapper;
        }
        public async Task<List<GetArrivalsDto>> GetUserAttendanceByMonthAsync(GetAttendanceByMonthDto monthDto) =>
            _mapperProfile.Map<List<GetArrivalsDto>>(await _attendanceRepository.GetUserAttendanceByMonthAsync(monthDto));


        public async Task<GetArrivalsDto> GetUserAttendanceByDateAsync(DateTime date) =>
            _mapperProfile.Map<GetArrivalsDto>(await _attendanceRepository.GetUserAttendanceByDateAsync(date));

        public async Task<GetAttendanceStatsDto> GetUserAttendanceStatsByMontAsync(GetAttendanceByMonthDto montDto) =>
            await _attendanceRepository.GetUserAttendanceStatsByMontAsync(montDto);
        
    }
}
