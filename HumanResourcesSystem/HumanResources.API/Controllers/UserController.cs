using HumanResources.Application.CQRS.IUserCommand;
using HumanResources.Application.CQRS.IUserHandler;
using HumanResources.Application.CQRS_User.Command;
using HumanResources.Application.CQRS_User.Handler;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{

    [Route("api/account")]
    [ApiController]
    public class UserController:ControllerBase
    {

        private readonly IUserHandlerService _userHandlerService;

        private readonly IUserCommandService _userCommandService;

        public UserController(IUserHandlerService userHandlerService, IUserCommandService userCommandService)
        {
            _userHandlerService = userHandlerService;
            _userCommandService = userCommandService;
        }

        public Task<IActionResult> GetInfromationsAboutCurrentUserAsync() 
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateUserInfromationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UserArrivalAsync()
        {
            throw new NotImplementedException();
        }
        

    }
}
