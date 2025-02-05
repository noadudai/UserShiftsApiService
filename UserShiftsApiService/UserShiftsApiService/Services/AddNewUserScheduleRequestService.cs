using System;
using System.Linq;
using UserShiftsApiService.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Services;

public class AddNewUserScheduleRequestService : IAddNewUserScheduleRequestService
{
    private readonly ShiftsSchedulingContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AddNewUserScheduleRequestService(ShiftsSchedulingContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddNewDateRangePreferenceRequestAsync(UserDateRangePreferenceRequestModel dateRangePreferenceRequest, string userId)
    {
        _dbContext.Add(new UserDateRangePreferenceRequestEntity
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            StartingDate = DateTime.Parse(dateRangePreferenceRequest.StartDate).ToUniversalTime(),
            EndingDate = DateTime.Parse(dateRangePreferenceRequest.EndDate).ToUniversalTime(),
            RequestType = dateRangePreferenceRequest.RequestType,
        });
        
        await _dbContext.SaveChangesAsync();
    }
}