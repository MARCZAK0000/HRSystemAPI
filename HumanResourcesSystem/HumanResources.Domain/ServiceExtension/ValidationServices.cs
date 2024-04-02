using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.AttendanceModelDto;
using HumanResources.Domain.Validations.AccountValidations;
using HumanResources.Domain.Validations.AttendanceValidation;
using HumanResources.Domain.UserModelDto;

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

            //User Validation
            services.AddScoped<IValidator<UpdateAccountInformationsDto>, UpdateUserInformationsValidator>();


            //Attendance Validaton
            services.AddScoped<IValidator<GetAttendanceByMonthDto>, GetAttendanceByMonthValidator>();
            services.AddScoped<IValidator<UserArrivalDto>,  UserArrivalValidator>();


        }
    }
}
