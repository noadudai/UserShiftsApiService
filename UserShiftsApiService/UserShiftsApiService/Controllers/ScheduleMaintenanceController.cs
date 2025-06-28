using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserShiftsApiService.Middlewares;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("/manager-schedule-maintenance/")]
public class ScheduleMaintenanceController : ControllerBase
{
    private readonly IManagerActionsService _managerActionsService;

    public ScheduleMaintenanceController(IManagerActionsService managerActionsService)
    {
        _managerActionsService = managerActionsService;
    }


    [HttpPost]
    [Route("create-schedule")]
    [Authorize]
    [ServiceFilter<UserContextProviderMiddleware>]
    public async Task<ActionResult> CreateNewShiftScheduleAsync(ScheduleModel schedule)
    {
        await _managerActionsService.CreateNewShiftScheduleAsync(schedule);
        return Ok();
    }
}