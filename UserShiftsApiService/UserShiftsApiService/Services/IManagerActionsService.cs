using System.Collections.Generic;
using System.Threading.Tasks;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public interface IManagerActionsService
{
    Task CreateNewShiftScheduleAsync(NewScheduleModel schedule);
    Task<SchedulesAndShiftsResponse> GetAllSchedulesAndShiftsAsync();
}