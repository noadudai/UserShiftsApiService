using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;
using UserShiftsApiService.UserContext;

namespace UserShiftsApiService.Services;

public class ManagerActionsService : IManagerActionsService
{
    private readonly ShiftsSchedulingContext _dbContext;
    private readonly IUserContextProvider _userContextProvider;

    
    public ManagerActionsService(ShiftsSchedulingContext dbContext, IUserContextProvider userContextProvider)
    {
        _dbContext = dbContext;
        _userContextProvider = userContextProvider;
    }
    
    public async Task CreateNewShiftScheduleAsync(CreateNewScheduleModel schedule)
    {
        var newSchedule = new ScheduleEntity
        {
            Id = Guid.NewGuid().ToString(),
            CreatedByManagerId = _userContextProvider.GetUserContext().UserId,
            CreationDate = DateTime.UtcNow,
        };

        var shiftEntities = schedule.Shifts.Select(shift => new ShiftEntity
        {
            Id = Guid.NewGuid().ToString(),
            ShiftType = shift.ShiftType,
            StartDate = shift.ShiftStartTime,
            EndDate = shift.ShiftEndTime,
            ScheduleId = newSchedule.Id
        });
        
        _dbContext.ShiftsSchedules.Add(newSchedule);
        _dbContext.Shifts.AddRange(shiftEntities);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<SchedulesResponseModel> GetAllSchedulesAsync()
    {
        var schedules = await _dbContext.ShiftsSchedules.GroupJoin(
            _dbContext.Shifts, 
            schedule => schedule.Id,
            shifts => shifts.ScheduleId,
            (schedule, shifts) => new 
            {
                Schedule = schedule,
                Shifts = shifts.ToList(),
            }).ToListAsync();
        
        var response = schedules.Select(
            group => new ScheduleResponseModel
            {
                Schedule = new ScheduleModel
                    {
                        Id = group.Schedule.Id, 
                        CreationDate = group.Schedule.CreationDate, 
                        CreatedByManagerId = group.Schedule.CreatedByManagerId
                        
                    }, 
                Shifts = group.Shifts.Select
                    (
                        shift => new ShiftModel
                        {
                            ShiftStartTime = shift.StartDate, 
                            ShiftEndTime = shift.EndDate, 
                            ShiftType = shift.ShiftType
                        }).ToArray()
            }).ToArray();
        
        return new SchedulesResponseModel { Schedules = response };
    }
}
