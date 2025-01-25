using System.Threading.Tasks;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public interface IAddNewUserScheduleRequestService
{
    Task AddNewVacationRequestAsync(UserDateRangeScheduleRequestModel dateRangeScheduleRequest, string userId, DateRangeRequestType requestType);
}