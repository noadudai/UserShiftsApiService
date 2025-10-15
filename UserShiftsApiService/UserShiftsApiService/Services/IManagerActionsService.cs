using System.Collections.Generic;
using System.Threading.Tasks;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public interface IManagerActionsService
{
    Task CreateNewShiftScheduleAsync(CreateNewShiftsScheduleModel shiftsSchedule);
    Task<SchedulesResponseModel> GetSchedulesAsync(ScheduleFetchingModel scheduleFetchingModel);
    Task ChangeShiftScheduleStatusAsync(ChangeShiftsScheduleStatusModel schedule);
    Task CreateNewWorkingScheduleAsync(CreateScheduleModel newSchedule);
}