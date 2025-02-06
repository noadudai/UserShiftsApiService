using System;
using System.Linq;
using UserShiftsApiService.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;
using UserShiftsApiService.UserContext;

namespace UserShiftsApiService.Services;

public class AddNewUserScheduleRequestService : IAddNewUserScheduleRequestService
{
    private readonly ShiftsSchedulingContext _dbContext;
    private readonly IUserContextProvider _userContextProvider;
    
    public AddNewUserScheduleRequestService(ShiftsSchedulingContext dbContext, IUserContextProvider userContextProvider)
    {
        _dbContext = dbContext;
        _userContextProvider = userContextProvider;
    }

    public async Task AddNewDateRangePreferenceRequestAsync(UserDateRangePreferenceRequestModel dateRangePreferenceRequest)
    {
        _dbContext.Add(new UserDateRangePreferenceRequestEntity
        {
            Id = Guid.NewGuid().ToString(),
            UserId = _userContextProvider.GetUserContext().UserId,
            StartingDate = DateTime.Parse(dateRangePreferenceRequest.StartDate).ToUniversalTime(),
            EndingDate = DateTime.Parse(dateRangePreferenceRequest.EndDate).ToUniversalTime(),
            RequestType = dateRangePreferenceRequest.RequestType,
        });
        
        await _dbContext.SaveChangesAsync();
    }
}