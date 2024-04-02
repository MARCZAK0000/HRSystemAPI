using HumanResources.Application.CQRS_User.Command;
using HumanResources.Application.CQRS_User.Handler;
using HumanResources.Domain.ModelDtos;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserHandlerService _userHandlerServices;

        private readonly IUserCommandService _userCommandServices;

        public UserController(IUserHandlerService userHandlerServices, IUserCommandService userCommandServices)
        {
            _userHandlerServices = userHandlerServices;
            _userCommandServices = userCommandServices;
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetInfromationsAboutUserAsync() 
        {
            var result = await _userHandlerServices.GetInfromationsAboutUserAsync();

            return Ok(result);
        }

        [HttpPut("info")]
        public async Task<IActionResult> UpdateInfromationsAboutUser([FromBody] UpdateAccountInformationsDto updateAccountInformations)
        {
            var result = await _userCommandServices.UpdateInfromationsAboutUserAsync(updateAccountInformations);

            if (!result)
            {
                return BadRequest();
            }

            return Created(string.Empty, new { result = true }); 
        }

       
         

        
    }
}
