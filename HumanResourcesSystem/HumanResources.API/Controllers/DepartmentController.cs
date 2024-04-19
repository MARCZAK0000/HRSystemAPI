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

        [HttpPost("update")]
        public async Task<IActionResult> ChangeDepartment([FromBody] ChangeDepartmentDto changeDepartment)
        {
            var result = await _departmentCommandSerivce.ChangeUserDepartmentAscyn(changeDepartment);

            return Ok(result);
        }
        [HttpGet("info")]
        public async Task<IActionResult> InfoAboutDepartmenst([FromQuery] int departmentID)
        {
            var result = await _departmentHandlerService.DepartmentInfoAsync(departmentID);

            return Ok(result);
        }
    }
}