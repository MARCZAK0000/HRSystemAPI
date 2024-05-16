using HumanResources.Domain.FinancialModelDto;
using HumanResources.Domain.Response;

namespace HumanResources.Domain.Repository
{
    public interface IEmployeePayRepository
    {
        Task<SetPaymentRateResponse> SetUserPaymentRateAsync(SetUserPaymentRateDto setUserPaymentRate, CancellationToken token);

        Task<SetPaymentRateResponse> UpdateUserPaymentRateAsync(SetUserPaymentRateDto updateUserPaymentRate, CancellationToken token);
    }
}
