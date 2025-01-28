using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("/user-schedule-preferences-request/")]
public class AddNewUserScheduleRequestController : ControllerBase
{
    private readonly IAddNewUserScheduleRequestService _addNewUserScheduleRequestService;

    public AddNewUserScheduleRequestController(IAddNewUserScheduleRequestService addNewUserScheduleRequestService)
    {
        _addNewUserScheduleRequestService = addNewUserScheduleRequestService;
    }

    [HttpPost]
    [Route("date-range-preference-request")]
    [Authorize]
    public async Task<IActionResult> AddNewDateRangePreferenceRequestAsync(UserDateRangePreferenceRequestModel dateRangePreferenceRequest, DateRangeRequestType dateRangeRequestType)
    {
        var userSub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _addNewUserScheduleRequestService.AddNewDateRangePreferenceRequestAsync(dateRangePreferenceRequest, userSub, dateRangeRequestType);

        return Ok("Date Range Request Added!");
    }
}