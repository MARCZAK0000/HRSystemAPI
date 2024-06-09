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
        public async Task<IActionResult> GetInfromationsAboutUserAsync(CancellationToken token) 
        {
            var result = await _userHandlerServices.GetInfromationsAboutUserAsync(token);

            return Ok(result);
        }

        [HttpPut("info")]
        public async Task<IActionResult> UpdateInfromationsAboutUser([FromBody] UpdateAccountInformationsDto updateAccountInformations, CancellationToken token)
        {
            var result = await _userCommandServices.UpdateInfromationsAboutUserAsync(updateAccountInformations, token);

            if (!result)
            {
                return BadRequest();
            }

            return Created(string.Empty, new { result = true }); 
        }



        [HttpPut("update")]
        public async Task<IActionResult> UpdateExperienceInformationsAboutUser([FromBody] UpdateExperienceInfomrationsDto update, CancellationToken token)
        {
            var result = await _userCommandServices.UpdateExperienceInformationsAboutUser(update, token);

            return Ok(result);  
        }

        [HttpPost("image/publish")]
        public async Task<IActionResult> UploadUserImage(IFormFile formFile, CancellationToken token)
        {
            var result = await _userCommandServices.UploadUserImageAsync(formFile, token);

            return Accepted(result);
        }

        [HttpPut("image/update")]
        public async Task<IActionResult> UpdateUserImage(IFormFile formFile, CancellationToken token)
        {
            var result = await _userCommandServices.UpdateUserImageAsync(formFile, token);

            return Accepted(result);
        }
        
    }
}
