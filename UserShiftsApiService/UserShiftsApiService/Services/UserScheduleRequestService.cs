using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;
using UserShiftsApiService.UserContext;

namespace UserShiftsApiService.Services;

public class UserScheduleRequestService : IUserScheduleRequestService
{
    private readonly ShiftsSchedulingContext _dbContext;
    private readonly IUserContextProvider _userContextProvider;
    
    public UserScheduleRequestService(ShiftsSchedulingContext dbContext, IUserContextProvider userContextProvider)
    {
        _dbContext = dbContext;
        _userContextProvider = userContextProvider;
    }

    public async Task<List<UserVacationModel>> GetAllUserVacationsByDateRangeAsync(
        UserDateRangePreferenceRequestModel vacationsDateRangeRequest)
    {
        var userId = _userContextProvider.GetUserContext().UserId;
        
        var vacations = await _dbContext.UserDateRangeScheduleRequests.Where(prefReq =>
                (prefReq.StartingDate >= vacationsDateRangeRequest.StartDate &&
                 prefReq.StartingDate <= vacationsDateRangeRequest.EndDate) ||
                (prefReq.StartingDate < vacationsDateRangeRequest.StartDate &&
                 prefReq.EndingDate >= vacationsDateRangeRequest.StartDate))
            .Where(prefRec => prefRec.RequestType == DateRangeRequestType.Vacation && prefRec.UserId == userId)
            .ToListAsync();

        var vacationsDates = vacations.Select(vacation => new UserVacationModel
            { StartDate = vacation.StartingDate, EndDate = vacation.EndingDate }).ToList();

        return vacationsDates;
    }
}