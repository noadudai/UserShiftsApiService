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
    private readonly IGetUserVacationsByMonthService _getUserVacationsByMonthService;
    
    public AddNewUserScheduleRequestController(IAddNewUserScheduleRequestService addNewUserScheduleRequestService, IGetUserVacationsByMonthService getUserVacationsByMonthService)
    {
        _addNewUserScheduleRequestService = addNewUserScheduleRequestService;
        _getUserVacationsByMonthService = getUserVacationsByMonthService;
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
    [Route("Vacations-this-month")]
    public async Task<IActionResult> GetUserVacationsByMonthAsync(DateTime month)
    {
        var vacations = await _getUserVacationsByMonthService.GetAllUerVacationsByMonthAsync(month);
        return Ok(vacations);
    }
}