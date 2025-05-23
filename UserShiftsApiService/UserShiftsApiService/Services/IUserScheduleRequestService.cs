using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public interface IUserScheduleRequestService
{
    Task<List<UserVacationModel>> GetAllUserVacationsByDateRangeAsync(UserDateRangePreferenceRequestModel vacationsDateRangeRequest);
}