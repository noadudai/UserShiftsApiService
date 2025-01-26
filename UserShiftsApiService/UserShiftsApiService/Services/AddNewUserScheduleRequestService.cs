using System;
using System.Linq;
using UserShiftsApiService.Models;
using System.Threading.Tasks;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Services;

public class AddNewUserScheduleRequestService : IAddNewUserScheduleRequestService
{
    private readonly ShiftsSchedulingContext _dbContext;
    
    public AddNewUserScheduleRequestService(ShiftsSchedulingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddNewVacationRequestAsync(UserDateRangeScheduleRequestModel dateRangeScheduleRequest, string userId, DateRangeRequestType requestType)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        
        _dbContext.Add(new UserDateRangeScheduleRequestEntity
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            StartingDate = DateTime.Parse(dateRangeScheduleRequest.VacationStartDate),
            EndingDate = DateTime.Parse(dateRangeScheduleRequest.VacationEndDate),
            User = user,
            RequestType = requestType,
        });
        
        await _dbContext.SaveChangesAsync();
    }
}