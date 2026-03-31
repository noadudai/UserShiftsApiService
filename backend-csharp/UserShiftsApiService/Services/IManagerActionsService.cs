using System.Threading.Tasks;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public interface IManagerActionsService
{
    Task CreateNewShiftScheduleAsync(CreateNewScheduleModel schedule);
    Task<SchedulesResponseModel> GetSchedulesAsync(ScheduleFetchingModel scheduleFetchingModel);
    Task ChangeShiftScheduleStatusAsync(ChangeShiftsScheduleStatusModel schedule);
    Task<EmployeesResponseModel> GetEmployeesAsync();
}