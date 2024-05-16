using AutoMapper;
using HumanResources.Domain.AttendanceModelDto;
using HumanResources.Domain.Repository;
using HumanResources.Domain.UserModelDto;

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
        public async Task<List<GetArrivalsDto>> GetUserAttendanceByMonthAsync(GetAttendanceByMonthDto monthDto, CancellationToken token) =>
            _mapperProfile.Map<List<GetArrivalsDto>>(await _attendanceRepository.GetUserAttendanceByMonthAsync(monthDto, token));


        public async Task<GetArrivalsDto> GetUserAttendanceByDateAsync(DateTime date, CancellationToken token) =>
            _mapperProfile.Map<GetArrivalsDto>(await _attendanceRepository.GetUserAttendanceByDateAsync(date, token));

        public async Task<GetAttendanceStatsDto> GetUserAttendanceStatsByMontAsync(GetAttendanceByMonthDto montDto, CancellationToken token) =>
            await _attendanceRepository.GetUserAttendanceStatsByMontAsync(montDto, token);

        public async Task<List<GetArrivalsDto>>
            GetUserCompletedOrNotAttendenceByMonthAsync(GetAttendanceByMonthDto monthDto, bool isCompleted, CancellationToken token) =>
            _mapperProfile.Map<List<GetArrivalsDto>>(await _attendanceRepository.GetUserCompletedOrNotAttendenceByMonthAsync(monthDto, isCompleted, token));

        public async Task<GetAttendanceStatsDto>
            GetInformationsAboutUserAttendanceByMonth(GetAttendanceByMonthDto monthDto, string userCode, CancellationToken token) =>
            await _attendanceRepository.GetInformationsAboutUserForLeadersAttendanceByMonth(monthDto, userCode, token);

    }
}
