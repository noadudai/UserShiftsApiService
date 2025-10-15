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
    [Route("create-shifts-schedule")]
    [Authorize]
    [ServiceFilter<UserContextProviderMiddleware>]
    public async Task<ActionResult> CreateNewShiftScheduleAsync(CreateNewShiftsScheduleModel shiftsSchedule)
    {
        await _managerActionsService.CreateNewShiftScheduleAsync(shiftsSchedule);
        return Ok();
    }
    
    [HttpGet]
    [Route("schedules")]
    [Authorize]
    [ServiceFilter<UserContextProviderMiddleware>]
    public async Task<ActionResult<SchedulesResponseModel>> GetSchedulesAsync([FromQuery] ScheduleFetchingModel fetchingOptions)
    {
        var schedules = await _managerActionsService.GetSchedulesAsync(fetchingOptions);
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
    
    [HttpPost]
    [Route("create-schedule")]
    public async Task<ActionResult> CreateWorkingScheduleAsync(CreateScheduleModel newSchedule)
    {
        await _managerActionsService.CreateNewWorkingScheduleAsync(newSchedule);
        return Ok();
    }
    
}