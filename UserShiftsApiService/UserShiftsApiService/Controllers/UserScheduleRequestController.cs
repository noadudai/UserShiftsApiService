using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Middlewares;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;
using UserShiftsApiService.UserContext;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("/user-schedule-preferences-request/")]
public class UserScheduleRequestController : ControllerBase
{
    private readonly IAddNewUserScheduleRequestService _addNewUserScheduleRequestService;
    private readonly IUserScheduleRequestService _userScheduleRequestService;
    
    public UserScheduleRequestController(IAddNewUserScheduleRequestService addNewUserScheduleRequestService, IUserScheduleRequestService userScheduleRequestService)
    {
        _addNewUserScheduleRequestService = addNewUserScheduleRequestService;
        _userScheduleRequestService = userScheduleRequestService;
    }

    [HttpPost]
    [Route("date-range-preference-request")]
    [Authorize]
    [ServiceFilter<UserContextProviderMiddleware>]
    public async Task<IActionResult> AddNewDateRangePreferenceRequestAsync(UserDateRangePreferenceRequestModel dateRangePreferenceRequest)
    {
        await _addNewUserScheduleRequestService.AddNewDateRangePreferenceRequestAsync(dateRangePreferenceRequest);

        return Ok("Date Range Request Added!");
    }

    [HttpPost]
    [Route("get-future-vacations")]
    [Authorize]
    [ServiceFilter<UserContextProviderMiddleware>]
    public async Task<ActionResult<UserVacationsResponse>> GetUserFutureVacationsAsync()
    {
        var vacations = await _userScheduleRequestService.GetAllUserFutureVacationsAsync();
        var response = new UserVacationsResponse { Vacations = vacations };
        return Ok(response);
    }
    
    [HttpPost]
    [Route("get-number-of-future-vacations")]
    [Authorize]
    [ServiceFilter<UserContextProviderMiddleware>]
    public async Task<ActionResult<int>> GetNumberOfUserFutureVacationsAsync()
    {
        var numberOfFutureVacations = await _userScheduleRequestService.GetNumberOfFutureVacationsAsync();
        return Ok(numberOfFutureVacations);
    }
}