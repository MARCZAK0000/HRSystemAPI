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
        public async Task<IActionResult> AddToManager([FromBody] RoleUpdateDto updateDto)
        {
            var role = RolesEnum.Manager;
            var result = await _adminCommandService.AddToRoleAsync(updateDto.UserId, role);

            return Ok(result);
        }
        [HttpPost("role/leader")]
        public async Task<IActionResult> AddToLeader([FromBody] RoleUpdateDto updateDto)
        {
            var role = RolesEnum.Leader;
            var result = await _adminCommandService.AddToRoleAsync(updateDto.UserId, role);

            return Ok(result);
        }

        [HttpPost("role/admin")]
        public async Task<IActionResult> AddToAdmin([FromBody] RoleUpdateDto updateDto)
        {
            var role = RolesEnum.Admin;
            var result = await _adminCommandService.AddToRoleAsync(updateDto.UserId, role);

            return Ok(result);
        }

        [HttpPost("role/remove/leader")]
        public async Task<IActionResult> RemoveRoleLeader([FromBody] RoleUpdateDto roleUpdateDto)
        {
            var role = RolesEnum.Leader;
            var result = await _adminCommandService.RemoveRoleAsync(roleUpdateDto.UserId, role);

            return Ok(result);
        }

        [HttpPost("role/remove/manager")]
        public async Task<IActionResult> RemoveRoleManager([FromBody] RoleUpdateDto roleUpdateDto)
        {
            var role = RolesEnum.Manager;
            var result = await _adminCommandService.RemoveRoleAsync(roleUpdateDto.UserId, role);

            return Ok(result);
        }

        [HttpPost("role/remove/admin")]
        public async Task<IActionResult> RemoveRoleAdmin([FromBody] RoleUpdateDto roleUpdateDto)
        {
            var role = RolesEnum.Admin;
            var result = await _adminCommandService.RemoveRoleAsync(roleUpdateDto.UserId, role);

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
