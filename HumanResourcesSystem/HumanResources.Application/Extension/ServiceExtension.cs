using Microsoft.Extensions.DependencyInjection;
using HumanResources.Application.CQRS.IUserCommand;
using HumanResources.Application.CQRS.UserCommand;
using HumanResources.Application.CQRS.IUserHandler;
using HumanResources.Application.CQRS.UserHandler;
using HumanResources.Application.Authentication;
using HumanResources.Application.AutoMapperProfile;
using HumanResources.Application.CQRS_User.Command;
using HumanResources.Application.CQRS_User.Handler;
using HumanResources.Application.CQRS_Attendance.Command;
using HumanResources.Application.CQRS_Attendance.Handler;
using HumanResources.Application.CQRS_Admin.Command;
using HumanResources.Application.CQRS_Admin.Handler;
using HumanResources.Application.EmailService;

namespace HumanResources.Application.Extension
{
    public static class ServiceExtension
    {   
        public static void AddApplication(this IServiceCollection service)
        {

            service.AddScoped<IUserContext, UserContex>();

            //Service for Account 
            service.AddScoped<IAccountCommandService, AccountCommandService>();
            service.AddScoped<IAccountHandlerService, AccountHandlerService>();


            //Service for User Informations
            service.AddScoped<IUserCommandService, UserCommandService>();
            service.AddScoped<IUserHandlerService, UserHandlerService>();


            //Service for Arrivals and Departure
            service.AddScoped<IAttendanceCommandService,  AttendanceCommandService>();  
            service.AddScoped<IAttendanceHandlerService,  AttendanceHandlerService>();

            //Service for Admin

            service.AddScoped<IAdminCommandService, AdminCommandSerivce>();
            service.AddScoped<IAdminHandlerService, AdminHandlerService>();

            //Service for Email
            service.AddScoped<IEmailServices, EmailServices>();

            service.AddAutoMapper(typeof(MapperProfile));

            //service.AddScoped(provider => new MapperConfiguration(cfg =>
            //{
            //    var scoped = provider.CreateScope();
            //    var userContext = scoped.ServiceProvider.GetService<IUserContext>();
            //    cfg.AddProfile(new MapperProfile(userContext!));
            //}));

        }
    }
}
