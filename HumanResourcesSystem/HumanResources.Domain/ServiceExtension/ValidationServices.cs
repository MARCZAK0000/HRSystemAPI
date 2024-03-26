using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Validations;

namespace HumanResources.Domain.ServiceExtension
{
    public static class ValidationServices
    {
        public static void AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();


            services.AddScoped<IValidator<RegisterAccountAsyncDto>, RegisterUserValidator>();
            services.AddScoped<IValidator<LoginAccountAsyncDto>, LoginUserValidator>();
            services.AddScoped<IValidator<ChangePasswordAsyncDto>, ChangePasswordValidator>();
            services.AddScoped<IValidator<ResetPasswordAsyncDto>, ResetPasswordValidator>();
            services.AddScoped<IValidator<ChangePhoneNumberDto>, ChangePhoneNumberValidator>();
            services.AddScoped<IValidator<UpdateAccountInformationsDto>, UpdateUserInformationsValidator>();
        }
    }
}
