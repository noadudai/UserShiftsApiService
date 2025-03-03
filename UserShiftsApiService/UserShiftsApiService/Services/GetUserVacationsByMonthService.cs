using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public class GetUserVacationsByMonthService : IGetUserVacationsByMonthService
{
    private readonly ShiftsSchedulingContext _dbContext;

    public GetUserVacationsByMonthService(ShiftsSchedulingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserDateRangePreferenceRequestEntity>> GetAllUerVacationsByMonthAsync(GetUserVacationMonthModel month)
    {
        return await _dbContext.UserDateRangeScheduleRequests.Where(prefRec =>
            prefRec.StartingDate.Date.Month == month.MonthNumber && prefRec.RequestType == DateRangeRequestType.Vacation).ToListAsync();
    }
}