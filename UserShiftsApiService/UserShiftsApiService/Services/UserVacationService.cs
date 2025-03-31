using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;
using UserShiftsApiService.UserContext;

namespace UserShiftsApiService.Services;

public class UserVacationService : IUserVacationService
{
    private readonly ShiftsSchedulingContext _dbContext;
    private readonly IUserContextProvider _userContextProvider;
    
    public UserVacationService(ShiftsSchedulingContext dbContext, IUserContextProvider userContextProvider)
    {
        _dbContext = dbContext;
        _userContextProvider = userContextProvider;
    }

    public async Task<List<OneVacationDateRangeModel>> GetAllUserVacationsByDateRangeAsync(
        UserDateRangePreferenceRequestModel vacationsDateRangeRequest)
    {
        var userId = _userContextProvider.GetUserContext().UserId;
        
        var vacations = await _dbContext.UserDateRangeScheduleRequests.Where(prefRec =>
                (prefRec.StartingDate >= vacationsDateRangeRequest.StartDate &&
                 prefRec.StartingDate <= vacationsDateRangeRequest.EndDate) ||
                (prefRec.StartingDate < vacationsDateRangeRequest.StartDate &&
                 prefRec.EndingDate >= vacationsDateRangeRequest.StartDate))
            .Where(prefRec => prefRec.RequestType == DateRangeRequestType.Vacation && prefRec.UserId == userId)
            .ToListAsync();

        var vacationsDates = vacations.Select(vacation => new OneVacationDateRangeModel
            { StartDate = vacation.StartingDate, EndDate = vacation.EndingDate }).ToList();

        return vacationsDates;
    }
}