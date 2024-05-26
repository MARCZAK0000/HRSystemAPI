using HumanResources.Application.CQRS_Admin.Command;
using HumanResources.Application.CQRS_Admin.Handler;
using HumanResources.Application.EmailService;
using HumanResources.Domain.AdminModelDto;
using HumanResources.Domain.EmailModelDto;
using HumanResources.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminHandlerService _adminHandlerService;
        private readonly IAdminCommandService _adminCommandService;

        private readonly IEmailServices _emailServices;

        public AdminController(IAdminHandlerService adminHandlerService, IAdminCommandService adminCommandService, IEmailServices emailServices)
        {
            _adminHandlerService = adminHandlerService;
            _adminCommandService = adminCommandService;
            _emailServices = emailServices;
        }

        [HttpPost("role/manager")]
        public async Task<IActionResult> AddToManager([FromBody] RoleUpdateDto updateDto, CancellationToken token)
        {
            var role = RolesEnum.Manager;
            var result = await _adminCommandService.AddToManagerAsync(updateDto.UserCode, role, token);

            return Ok(result);
        }
        [HttpPost("role/leader")]
        public async Task<IActionResult> AddToLeader([FromBody] RoleUpdateDto updateDto, CancellationToken token)
        {
            var role = RolesEnum.Leader;
            var result = await _adminCommandService.AddToLeaderAsync(updateDto.UserCode, role, token);

            return Ok(result);
        }

        [HttpPost("role/admin")]
        public async Task<IActionResult> AddToAdmin([FromBody] RoleUpdateDto updateDto, CancellationToken token)
        {
            var role = RolesEnum.Admin;
            var result = await _adminCommandService.AddToAdminAsync(updateDto.UserCode, role, token);

            return Ok(result);
        }

        [HttpPost("role/remove/leader")]
        public async Task<IActionResult> RemoveRoleLeader([FromBody] RoleUpdateDto roleUpdateDto, CancellationToken token)
        {
            var role = RolesEnum.Leader;
            var result = await _adminCommandService.RemoveLeaderAsync(roleUpdateDto.UserCode, role, token);

            return Ok(result);
        }

        [HttpPost("role/remove/manager")]
        public async Task<IActionResult> RemoveRoleManager([FromBody] RoleUpdateDto roleUpdateDto , CancellationToken token)
        {
            var role = RolesEnum.Manager;
            var result = await _adminCommandService.RemoveManagerAsync(roleUpdateDto.UserCode, role, token);

            return Ok(result);
        }

        [HttpPost("role/remove/admin")]
        public async Task<IActionResult> RemoveRoleAdmin([FromBody] RoleUpdateDto roleUpdateDto , CancellationToken token)
        {
            var role = RolesEnum.Admin;
            var result = await _adminCommandService.RemoveAdminAsync(roleUpdateDto.UserCode, role, token);

            return Ok(result);
        }

        [HttpPost("email")]
        public async Task<IActionResult> SendEmailTestAsync([FromBody] SendEmailDto emailResponse, CancellationToken token)
        {
            var result = await _emailServices.SendEmailAsync(emailResponse, token);

            return Ok(result);
        }
    }
}
