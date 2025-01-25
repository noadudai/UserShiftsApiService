using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("/user-schedule-request/")]
public class AddNewUserScheduleRequestController : ControllerBase
{
    private readonly IAddNewUserScheduleRequestService _addNewUserScheduleRequestService;

    public AddNewUserScheduleRequestController(IAddNewUserScheduleRequestService addNewUserScheduleRequestService)
    {
        _addNewUserScheduleRequestService = addNewUserScheduleRequestService;
    }

    [HttpPost]
    [Route("vacation-request")]
    [Authorize]
    public async Task<IActionResult> AddNewVacationRequestAsync(UserDateRangeScheduleRequestModel dateRangeScheduleRequest)
    {
        var userSub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _addNewUserScheduleRequestService.AddNewVacationRequestAsync(dateRangeScheduleRequest, userSub, DateRangeRequestType.Vacation);

        return Ok("Vacation Added!");
    }
}