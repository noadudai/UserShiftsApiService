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
public class AddNewUserScheduleRequestController : ControllerBase
{
    private readonly IAddNewUserScheduleRequestService _addNewUserScheduleRequestService;
    private readonly IGetUserVacationsByDateRangeService _getUserVacationsByDateRangeService;
    
    public AddNewUserScheduleRequestController(IAddNewUserScheduleRequestService addNewUserScheduleRequestService, IGetUserVacationsByDateRangeService getUserVacationsByDateRangeService)
    {
        _addNewUserScheduleRequestService = addNewUserScheduleRequestService;
        _getUserVacationsByDateRangeService = getUserVacationsByDateRangeService;
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
    [Route("Vacations-by-date-range")]
    public async Task<ActionResult<UserVacationsResponse>> GetUserVacationsInDateRangeAsync(UserDateRangePreferenceRequestModel vacationsDateRangeRequest)
    {
        var vacations = await _getUserVacationsByDateRangeService.GetAllUserVacationsByDateRangeAsync(vacationsDateRangeRequest);
        var response = new UserVacationsResponse { Vacations = vacations };
        return Ok(response);
    }
}