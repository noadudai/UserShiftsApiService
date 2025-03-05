using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public class GetUserVacationsByDateRangeService : IGetUserVacationsByDateRangeService
{
    private readonly ShiftsSchedulingContext _dbContext;

    public GetUserVacationsByDateRangeService(ShiftsSchedulingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<OneVacationDateRangeModel>> GetAllUserVacationsByDateRangeAsync(UserDateRangePreferenceRequestModel vacationsDateRangeRequest)
    {
        var vacations = await _dbContext.UserDateRangeScheduleRequests.Where(prefRec =>
            prefRec.StartingDate >= vacationsDateRangeRequest.StartDate && prefRec.StartingDate <= vacationsDateRangeRequest.EndDate && prefRec.RequestType == DateRangeRequestType.Vacation).ToListAsync();
        
        var vacationsDates = vacations.Select(vacation => new OneVacationDateRangeModel{StartDate = vacation.StartingDate, EndDate = vacation.EndingDate}).ToList();

        return vacationsDates;
    }
}