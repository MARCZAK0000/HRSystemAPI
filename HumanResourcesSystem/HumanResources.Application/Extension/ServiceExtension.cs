using Microsoft.Extensions.DependencyInjection;
using HumanResources.Application.CQRS.IUserCommand;
using HumanResources.Application.CQRS.UserCommand;
using HumanResources.Application.CQRS.IUserHandler;
using HumanResources.Application.CQRS.UserHandler;
using HumanResources.Application.Authentication;
using AutoMapper;
using HumanResources.Application.AutoMapperProfile;

namespace HumanResources.Application.Extension
{
    public static class ServiceExtension
    {   
        public static void AddApplication(this IServiceCollection service)
        {


            service.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scoped = provider.CreateScope();
                var userContext = scoped.ServiceProvider.GetService<IUserContext>();
                cfg.AddProfile(new MapperProfile(userContext!));
            }));


            service.AddScoped<IUserContext, UserContex>();
            service.AddScoped<IUserCommandService, UserCommandService>();
            service.AddScoped<IUserHandlerService, UserHandlerService>();
        }
    }
}
