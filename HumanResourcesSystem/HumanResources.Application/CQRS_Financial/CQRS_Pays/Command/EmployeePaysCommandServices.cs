using AutoMapper;
using HumanResources.Domain.FinancialModelDto;
using HumanResources.Domain.Repository;
using HumanResources.Domain.Response;

namespace HumanResources.Application.CQRS_Financial.CQRS_Pays.Command
{
    public class EmployeePaysCommandServices : IEmployeePaysCommandServices
    {
        private readonly IEmployeePayRepository _employeePayRepository;

        public EmployeePaysCommandServices(IEmployeePayRepository employeePayRepository)
        {
            _employeePayRepository = employeePayRepository;
        }

        public async Task<SetPaymentRateResponse> SetUserPaymentRateAsync(SetUserPaymentRateDto setUserPaymentRate, CancellationToken token) =>
            await _employeePayRepository.SetUserPaymentRateAsync(setUserPaymentRate, token);

        public async Task<SetPaymentRateResponse> UpdateUserPaymentRateAsync(SetUserPaymentRateDto updateUserPaymentRate, CancellationToken token) =>
            await _employeePayRepository.UpdateUserPaymentRateAsync(updateUserPaymentRate, token);
    }
}
