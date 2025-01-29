using System;
using System.Linq;
using UserShiftsApiService.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Services;

public class AddNewUserScheduleRequestService : IAddNewUserScheduleRequestService
{
    private readonly ShiftsSchedulingContext _dbContext;
    
    public AddNewUserScheduleRequestService(ShiftsSchedulingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddNewDateRangePreferenceRequestAsync(UserDateRangePreferenceRequestModel dateRangePreferenceRequest, string userId)
    {
        var user = _dbContext.Users.First(u => u.AuthSub == userId);
        
        _dbContext.Add(new UserDateRangePreferenceRequestEntity
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            StartingDate = DateTime.Parse(dateRangePreferenceRequest.StartDate).ToUniversalTime(),
            EndingDate = DateTime.Parse(dateRangePreferenceRequest.EndDate).ToUniversalTime(),
            RequestType = dateRangePreferenceRequest.RequestType,
        });
        
        await _dbContext.SaveChangesAsync();
    }
}