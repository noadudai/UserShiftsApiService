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

    public async Task<List<UserDateRangePreferenceRequestEntity>> GetAllUserVacationsByDateRangeAsync(GetVacationsByDateRangeModel vacationsDateRange)
    {
        return await _dbContext.UserDateRangeScheduleRequests.Where(prefRec =>
            prefRec.StartingDate >= vacationsDateRange.StartDateOfRange && prefRec.StartingDate <= vacationsDateRange.EndDateOfRange && prefRec.RequestType == DateRangeRequestType.Vacation).ToListAsync();
    }
}