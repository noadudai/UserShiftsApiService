using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserShiftsApiService.ActionFilters;
using UserShiftsApiService.Middlewares;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("/manager-employees-actions/")]
public class ManagerEmployeesActionsController : ControllerBase
{
    private readonly IManagerActionsService _managerActionsService;

    public ManagerEmployeesActionsController(IManagerActionsService managerActionsService)
    {
        _managerActionsService = managerActionsService;
    }

    [HttpGet]
    [Route("employees")]
    [Authorize(Policy = "ManagerOnly")]
    [ServiceFilter<UserContextProviderMiddleware>]
    [ServiceFilter<RequireManagerDbRoleFilter>]
    public async Task<ActionResult<EmployeesResponseModel>> GetEmployeesAsync()
    {
        var employees = await _managerActionsService.GetEmployeesAsync();
        return Ok(employees);
    }
}
