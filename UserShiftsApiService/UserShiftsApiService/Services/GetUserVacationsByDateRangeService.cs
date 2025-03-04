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

    public async Task<List<Tuple<DateTime,DateTime>>> GetAllUserVacationsByDateRangeAsync(GetVacationsByDateRangeModel vacationsDateRange)
    {
        var vacations = await _dbContext.UserDateRangeScheduleRequests.Where(prefRec =>
            prefRec.StartingDate >= vacationsDateRange.StartDateOfRange && prefRec.StartingDate <= vacationsDateRange.EndDateOfRange && prefRec.RequestType == DateRangeRequestType.Vacation).ToListAsync();
        
        var vacationsDates = vacations.Select(vacation => new Tuple<DateTime, DateTime>(vacation.StartingDate, vacation.EndingDate)).ToList();

        return vacationsDates;
    }
}