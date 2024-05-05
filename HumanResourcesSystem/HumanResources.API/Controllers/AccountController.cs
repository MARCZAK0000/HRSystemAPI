using HumanResources.Application.CQRS.IUserCommand;
using HumanResources.Application.CQRS.IUserHandler;
using HumanResources.Domain.ModelDtos;
using Microsoft.AspNetCore.Mvc;


namespace HumanResources.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountCommandService _accountCommandService;

        private readonly IAccountHandlerService _accountHandlerService;

        public AccountController(IAccountCommandService userCommandService, IAccountHandlerService userHandlerService)
        {
            _accountCommandService = userCommandService;
            _accountHandlerService = userHandlerService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterAccountAsyncDto register)
        {

            var result = await _accountCommandService.RegisterUserAsync(register);


            if (!result.Result)
            {
                
                return BadRequest(result);
            }
            return Created("",result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInUser([FromBody] LoginAccountAsyncDto login)
        {

            var result = await _accountCommandService.SignInUserAsync(login);

            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
       
        [HttpGet("token")]
        public async Task<IActionResult> GenerateConfirmEmailToken()
        {

            var result = await _accountHandlerService.GenerateConfirmEmailTokenAsync();
            
            return Ok(result);
        }
        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailAsyncDto confirm)
        {
            if(confirm.Email == null || confirm.Token == null) 
            {
                return BadRequest();
            }
            var result = await _accountCommandService.ConfirmEmailAsync(confirm.Email, confirm.Token);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("change/password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordAsyncDto changePassword)
        {
          
            var result = await _accountCommandService.ChangePasswordAsync(changePassword);

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
            var result = await _accountHandlerService.GenerateForgetPasswordTokenAsync(email, phone);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("forget_password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ResetPasswordAsyncDto resetPassword)
        {

            var result = await _accountCommandService.ForgetPasswordAsync(resetPassword);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("change/phone")]
        public async Task<IActionResult> GenerateChangePhoneNumberToken([FromBody] ChangePhoneNumberDto change)
        {
            var result = await _accountHandlerService.GeneratePhoneNumberChangeTokenAsync(change);

            if (!result.Result)
            {
                return BadRequest();
            }
            return Ok(result);
        }

    }
}
