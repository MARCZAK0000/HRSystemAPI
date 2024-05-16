using HumanResources.Application.CQRS_Departmens.Command;
using HumanResources.Application.CQRS_Departmens.Handler;
using HumanResources.Domain.DepartmentModelDto;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentCommandService _departmentCommandSerivce;

        private readonly IDepartmentHandlerService _departmentHandlerService;

        public DepartmentController(IDepartmentCommandService departmentCommandSerivce, IDepartmentHandlerService departmentHandlerService)
        {
            _departmentCommandSerivce = departmentCommandSerivce;
            _departmentHandlerService = departmentHandlerService;
        }

        [HttpPost("user/update")]
        public async Task<IActionResult> ChangeUserDepartment([FromBody] ChangeDepartmentDto changeDepartment, CancellationToken token)
        {
            var result = await _departmentCommandSerivce.ChangeUserDeparmentAsync(changeDepartment, token);

            return Ok(result);
        }
        [HttpGet("info/{departmentID}")]
        public async Task<IActionResult> InfoAboutDepartmenst([FromRoute] int departmentID, CancellationToken token)
        {
            var result = await _departmentHandlerService.DepartmentInfoAsync(departmentID, token);

            return Ok(result);
        }
        [HttpGet("info")]
        public async Task<IActionResult> GetAllDepartmenst(CancellationToken token)
        {
            var result = await _departmentHandlerService.GetAllDepartmenst(token);

            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddDeparment([FromBody] DepartmentUpdateDto departmentAdd, CancellationToken token)
        {
            var result = await _departmentCommandSerivce.AddDepartmentAsync(departmentAdd, token);

            return Ok(result);
        }

        [HttpPut("update/{deparmentId}")]
        public async Task<IActionResult> UpdateDeparment([FromBody] DepartmentUpdateDto update, [FromRoute] int deparmentId, CancellationToken token)
        {
            var result = await _departmentCommandSerivce.UpdateDepartmentAsync(update, deparmentId, token);

            return Ok(result);
        }

        
    }
}