using HumanResources.Application.CQRS_Departmens.Command;
using HumanResources.Application.CQRS_Departmens.Handler;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmenController : ControllerBase
    {
        private readonly IDepartmentCommandService _departmentCommandSerivce;

        private readonly IDepartmentHandlerService _departmentHandlerService;

        public DepartmenController(IDepartmentCommandService departmentCommandSerivce, IDepartmentHandlerService departmentHandlerService)
        {
            _departmentCommandSerivce = departmentCommandSerivce;
            _departmentHandlerService = departmentHandlerService;
        }

    }
}
