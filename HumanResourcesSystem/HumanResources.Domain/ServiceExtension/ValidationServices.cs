using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.AttendanceModelDto;
using HumanResources.Domain.Validations.AccountValidations;
using HumanResources.Domain.Validations.AttendanceValidation;
using HumanResources.Domain.UserModelDto;
using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Validations.AttendanceValidatons;
using HumanResources.Domain.Validations.AbsenceValidatons;
using HumanResources.Domain.FinancialModelDto;
using HumanResources.Domain.Validations.FinancialValidations;

namespace HumanResources.Domain.ServiceExtension
{
    public static class ValidationServices
    {
        public static void AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            //Account Validation
            services.AddScoped<IValidator<RegisterAccountAsyncDto>, RegisterUserValidator>();
            services.AddScoped<IValidator<LoginAccountAsyncDto>, LoginUserValidator>();
            services.AddScoped<IValidator<ChangePasswordAsyncDto>, ChangePasswordValidator>();
            services.AddScoped<IValidator<ResetPasswordAsyncDto>, ResetPasswordValidator>();
            services.AddScoped<IValidator<ChangePhoneNumberDto>, ChangePhoneNumberValidator>();
            services.AddScoped<IValidator<GenerateForgetPasswordDto>, GenerateForgetPasswordValidator>();
            services.AddScoped<IValidator<RefreshTokenDto>, RefreshTokenValidator>();
            services.AddScoped<IValidator<UpdateExperienceInfomrationsDto>, UpdateExperienceInfomrationsValidator>();
            //User Validation
            services.AddScoped<IValidator<UpdateAccountInformationsDto>, UpdateUserInformationsValidator>();


            //Attendance Validaton
            services.AddScoped<IValidator<GetAttendanceByMonthDto>, GetAttendanceByMonthValidator>();
            services.AddScoped<IValidator<UserArrivalDto>,  UserArrivalValidator>();

            //Absence Validation
            services.AddScoped<IValidator<CreateAbsenceDto>, CreateAbsenceValidator>();
            services.AddScoped<IValidator<AbsenceDecisionInfoDto>, AbsenceDecisionInfoValidator>();
            services.AddScoped<IValidator<ShowAbsenceByIdDto>,ShowAbsenceByIdValidator>();


            //Financial Validation
            services.AddScoped<IValidator<SetUserPaymentRateDto>, SetUserPaymentRateValidator>();
            services.AddScoped<IValidator<GetExchangeRateAsyncDto>, GetExchangeRateValidator>();
        }
    }
}
