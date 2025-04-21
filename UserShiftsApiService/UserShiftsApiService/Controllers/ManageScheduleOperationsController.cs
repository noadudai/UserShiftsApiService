using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("/manage-schedule-operations/")]
public class ManageScheduleOperationsController : ControllerBase
{
    private readonly IShiftService _shiftService;

    public ManageScheduleOperationsController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }
    
    [HttpPost]
    [Route("create-new-shift")]
    public async Task<IActionResult> CreateNewShiftAsync(ShiftModel shiftModel)
    {
        await _shiftService.CreateNewShiftAsync(shiftModel);
        
        return Ok("Shift Saved To DB!");
    }
}