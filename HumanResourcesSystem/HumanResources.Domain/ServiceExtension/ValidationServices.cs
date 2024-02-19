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


            services.AddScoped<IValidator<RegisterUserAsyncDto>, RegisterUserValidator>();
            services.AddScoped<IValidator<LoginUserAsyncDto>, LoginUserValidator>();
        }
    }
}
