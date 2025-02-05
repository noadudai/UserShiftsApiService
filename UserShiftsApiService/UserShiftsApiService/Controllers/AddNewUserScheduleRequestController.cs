using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _userId;

    public AddNewUserScheduleRequestController(IAddNewUserScheduleRequestService addNewUserScheduleRequestService, IHttpContextAccessor httpContextAccessor)
    {
        _addNewUserScheduleRequestService = addNewUserScheduleRequestService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    [Route("date-range-preference-request")]
    [Authorize]
    public async Task<IActionResult> AddNewDateRangePreferenceRequestAsync(UserDateRangePreferenceRequestModel dateRangePreferenceRequest)
    {
        await _addNewUserScheduleRequestService.AddNewDateRangePreferenceRequestAsync(dateRangePreferenceRequest, _userId);

        return Ok("Date Range Request Added!");
    }
}