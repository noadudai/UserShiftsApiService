using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> CreateNewShiftScheduleAsync(ShiftsScheduleModel shiftScheduleModel)
    {
        await _managerActionsService.CreateNewShiftScheduleAsync(shiftScheduleModel);
        return Ok();
    }
}