using HumanResources.Application.CQRS.IUserCommand;
using HumanResources.Application.CQRS.IUserHandler;
using HumanResources.Domain.ModelDtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HumanResources.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountCommandService _userCommandService;

        private readonly IAccountHandlerService _userHandlerService;

        public AccountController(IAccountCommandService userCommandService, IAccountHandlerService userHandlerService)
        {
            _userCommandService = userCommandService;
            _userHandlerService = userHandlerService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterAccountAsyncDto register)
        {

            var result = await _userCommandService.RegisterUserAsync(register);


            if (!result.Result)
            {
                
                return BadRequest(result);
            }
            return Created("",result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInUser([FromBody] LoginAccountAsyncDto login)
        {

            var result = await _userCommandService.SignInUserAsync(login);

            if (!result.Result)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpPut("informations")]
        public async Task<IActionResult> UpdateUserInfromations([FromBody] UpdateAccountInformationsDto updateAccountInformations)
        {
            var result = await _userCommandService.UpdateUserInfromationsAsync(updateAccountInformations);

            if(!result)
            {
                return BadRequest();
            }
            return Created(string.Empty, new { Result = result, Message = "Well Done"});
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> GenerateConfirmEmailToken([FromQuery] string email)
        {
            if(email == null)
            {
                return BadRequest(); 
            }

            var result = await _userHandlerService.GenerateConfirmEmailTokenAsync(email);
            if (!result.Result)
            {
                return BadRequest(result);  
            }
            return Ok(result);
        }
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailAsyncDto confirm)
        {
            if(confirm.Email == null || confirm.Token == null) 
            {
                return BadRequest();
            }
            var result = await _userCommandService.ConfirmEmailAsync(confirm.Email, confirm.Token);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("change/password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordAsyncDto changePassword)
        {
          
            var result = await _userCommandService.ChangePasswordAsync(changePassword);

            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("forget_password")]
        public async Task<IActionResult> GenerateForgetPasswordToken([FromQuery] string email, [FromQuery] string phone)
        {
            if (email == null || phone == null)
            {
                return BadRequest();
            }
            var result = await _userHandlerService.GenerateForgetPasswordTokenAsync(email, phone);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPatch("forget_password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ResetPasswordAsyncDto resetPassword)
        {

            var result = await _userCommandService.ForgetPasswordAsync(resetPassword);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("change/phone")]
        public async Task<IActionResult> GenerateChangePhoneNumberToken([FromBody] ChangePhoneNumberDto change)
        {
            var result = await _userHandlerService.GeneratePhoneNumberChangeTokenAsync(change);

            if (!result.Result)
            {
                return BadRequest();
            }
            return Ok(result);
        }

    }
}
