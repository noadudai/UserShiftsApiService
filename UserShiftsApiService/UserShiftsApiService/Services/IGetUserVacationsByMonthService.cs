using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Services;

public interface IGetUserVacationsByMonthService
{
    Task<List<UserDateRangePreferenceRequestEntity>> GetAllUerVacationsByMonthAsync(DateTime month);
}