using AutoMapper;
using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Repository;

namespace HumanResources.Application.CQRS_Absence.Command
{
    public class AbsenceCommandService : IAbsenceCommandService
    {
        private readonly IAbsenceRepository _absenceRepository;

        private readonly IMapper _mapper;

        public AbsenceCommandService(IMapper mapper, IAbsenceRepository absenceRepository)
        {
            _mapper = mapper;
            _absenceRepository = absenceRepository;
        }

        public async Task<AbsenceInfoDto> CreateAbsenceAsync(CreateAbsenceDto createAbsence, CancellationToken token) =>
            _mapper.Map<AbsenceInfoDto>(await _absenceRepository.CreateAbsenceAsync(createAbsence, token));

        public async Task<AbsenceDecisionDto> AbsenceDecisionAsync(AbsenceDecisionInfoDto absenceDecision, CancellationToken token) =>
            _mapper.Map<AbsenceDecisionDto>(await _absenceRepository.AbsenceDecisionAsync(absenceDecision, token));

    }
}
