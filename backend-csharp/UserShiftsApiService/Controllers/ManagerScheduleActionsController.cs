using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserShiftsApiService.ActionFilters;
using UserShiftsApiService.Middlewares;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("/manager-schedule-actions/")]
public class ManagerScheduleActionsController : ControllerBase
{
    private readonly IManagerActionsService _managerActionsService;

    public ManagerScheduleActionsController(IManagerActionsService managerActionsService)
    {
        _managerActionsService = managerActionsService;
    }


    [HttpPost]
    [Route("create-schedule")]
    [Authorize(Policy = "ManagerOnly")]
    [ServiceFilter<UserContextProviderMiddleware>]
    [ServiceFilter<RequireManagerDbRoleFilter>]
    public async Task<ActionResult> CreateNewShiftScheduleAsync(CreateNewScheduleModel schedule)
    {
        await _managerActionsService.CreateNewShiftScheduleAsync(schedule);
        return Ok();
    }
    
    [HttpGet]
    [Route("schedules")]
    [Authorize(Policy = "ManagerOnly")]
    [ServiceFilter<UserContextProviderMiddleware>]
    [ServiceFilter<RequireManagerDbRoleFilter>]
    public async Task<ActionResult<SchedulesResponseModel>> GetSchedulesAsync(ScheduleFetchingModel scheduleFetchingModel)
    {
        var schedules = await _managerActionsService.GetSchedulesAsync(scheduleFetchingModel);
        return Ok(schedules);
    }

    [HttpPost]
    [Route("change-schedule-status")]
    [Authorize(Policy = "ManagerOnly")]
    [ServiceFilter<UserContextProviderMiddleware>]
    [ServiceFilter<RequireManagerDbRoleFilter>]
    public async Task<ActionResult> ChangeShiftScheduleStatusAsync(ChangeShiftsScheduleStatusModel schedule)
    {
        await _managerActionsService.ChangeShiftScheduleStatusAsync(schedule);
        return Ok();
    }
}