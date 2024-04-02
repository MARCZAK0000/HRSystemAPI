using AutoMapper;
using HumanResources.Domain.Repository;
using HumanResources.Domain.UserModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_Attendance.Command
{
    public class AttendanceCommandService : IAttendanceCommandService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        private readonly IMapper _mapper;

        public AttendanceCommandService(IAttendanceRepository attendanceRepository, 
            IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        public async Task<bool> UserArrivalAsync(UserArrivalDto userArrival)
            => await _attendanceRepository.UserArrivalAsync(userArrival);

        public async Task<bool> UserDepartureAsync(UserDepartureDto userDeparture)
            => await _attendanceRepository.UserDepartureAsync(userDeparture);

    }
}
