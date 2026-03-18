using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    [ServiceFilter<UserContextProviderMiddleware>]
    public async Task<ActionResult> CreateNewShiftScheduleAsync(CreateNewScheduleModel schedule)
    {
        await _managerActionsService.CreateNewShiftScheduleAsync(schedule);
        return Ok();
    }
    
    [HttpGet]
    [Route("schedules")]
    [Authorize]
    [ServiceFilter<UserContextProviderMiddleware>]
    public async Task<ActionResult<SchedulesResponseModel>> GetSchedulesAsync([FromBody] ScheduleFetchingModel scheduleFetchingModel)
    {
        var schedules = await _managerActionsService.GetSchedulesAsync(scheduleFetchingModel);
        return Ok(schedules);
    }

    [HttpPost]
    [Route("change-schedule-status")]
    [Authorize]
    [ServiceFilter<UserContextProviderMiddleware>]
    public async Task<ActionResult> ChangeShiftScheduleStatusAsync(ChangeShiftsScheduleStatusModel schedule)
    {
        await _managerActionsService.ChangeShiftScheduleStatusAsync(schedule);
        return Ok();
    }
}