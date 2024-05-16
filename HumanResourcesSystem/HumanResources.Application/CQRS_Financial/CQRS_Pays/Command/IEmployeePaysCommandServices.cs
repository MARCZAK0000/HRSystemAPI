using HumanResources.Domain.FinancialModelDto;
using HumanResources.Domain.Response;

namespace HumanResources.Application.CQRS_Financial.CQRS_Pays.Command
{
    public interface IEmployeePaysCommandServices
    {
        Task<SetPaymentRateResponse> SetUserPaymentRateAsync(SetUserPaymentRateDto setUserPaymentRate, CancellationToken token);

        Task<SetPaymentRateResponse> UpdateUserPaymentRateAsync(SetUserPaymentRateDto updateUserPaymentRate, CancellationToken token);
    }
}
