using HumanResources.Application.CQRS.IUserCommand;
using HumanResources.Application.CQRS.IUserHandler;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HumanResources.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCommandService _userCommandService;

        private readonly IUserHandlerService _userHandlerService;

        public UserController(IUserCommandService userCommandService, IUserHandlerService userHandlerService)
        {
            _userCommandService = userCommandService;
            _userHandlerService = userHandlerService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserAsyncDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _userCommandService.RegisterUserAsync(register);


            if (!result.Result)
            {
                
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInUser([FromBody] LoginUserAsyncDto login)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest();
            }

            var result = await _userCommandService.SignInUserAsync(login);

            if (!result.Result)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpGet("account/confirm")]
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
        [HttpPost("account/confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email , [FromBody] string token)
        {
            if(email == null || token == null) 
            {
                return BadRequest();
            }
            var result = await _userCommandService.ConfirmEmailAsync(email, token);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("account/change/password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordAsyncDto changePassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _userCommandService.ChangePasswordAsync(changePassword);

            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("account/forget")]
        public async Task<IActionResult> GenerateForgetPasswordTokenAsync([FromQuery] string email, [FromQuery] string phonenumer)
        {
            if (email == null || phonenumer == null)
            {
                return BadRequest();
            }
            var result = await _userHandlerService.GenerateForgetPasswordTokenAsync(email, phonenumer);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPatch("account/forget")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordNewPasswordAsyncDto resetPassword)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            var result = await _userCommandService.ForgetPasswordAsync(resetPassword);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("account")]
        public async Task<IActionResult> GetInformationsAboutUser()
        {
            throw new NotImplementedException();    
        }
    }
}
