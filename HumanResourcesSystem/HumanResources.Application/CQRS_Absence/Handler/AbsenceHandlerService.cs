using AutoMapper;
using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Repository;

namespace HumanResources.Application.CQRS_Absence.Handler
{
    public class AbsenceHandlerService : IAbsenceHandlerService
    {
        private readonly IAbsenceRepository _absenceRepository;

        private readonly IMapper _mapper;

        public AbsenceHandlerService(IAbsenceRepository absenceRepository,
            IMapper mapper)
        {
            _absenceRepository = absenceRepository;
            _mapper = mapper;
        }

        public async Task<List<AbsenceInfoDto>> ShowAbsencesByYearAsync(int year) =>
            _mapper.Map<List<AbsenceInfoDto>>(await _absenceRepository.ShowAbsencesByYearAsync(year));
    }
}
