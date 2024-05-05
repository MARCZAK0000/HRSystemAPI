using AutoMapper;
using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Cache;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Repository;
using Microsoft.Extensions.Caching.Memory;

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

        public async Task<MemoryStream> GeneratePdfReportAsync(string userID, int year)
        {
            var list = _mapper.Map<List<AbsenceInfoDto>>(await _absenceRepository.ShowAbsencesByYearAsync(userID, year));
            var result = await _absenceRepository.GenerateAbsenceReportPDF(list, (userID, year));
            return result;
        }
           

        public async Task<List<AbsenceInfoDto>> ShowAbsencesByYearAsync(string userID, int year) => 
            _mapper.Map<List<AbsenceInfoDto>>(await _absenceRepository.ShowAbsencesByYearAsync(userID, year));
            
    }
}
