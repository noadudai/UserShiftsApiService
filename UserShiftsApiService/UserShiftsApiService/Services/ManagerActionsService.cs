using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public class ManagerActionsService : IManagerActionsService
{
    private readonly ShiftsSchedulingContext _dbContext;
    
    public ManagerActionsService(ShiftsSchedulingContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateNewShiftScheduleAsync(ShiftsScheduleModel shiftsScheduleModel)
    {
        var newSchedule = new ScheduleEntity
        {
            Id = Guid.NewGuid().ToString(),
            ShiftsInSchedule = shiftsScheduleModel.Shifts.Select((shift) => new ShiftEntity
            {
                Id = Guid.NewGuid().ToString(),
                ShiftType = shift.ShiftType,
                StartDate = shift.ShiftStartTime,
                EndDate = shift.ShiftEndTime,
            }).ToList()
        };
        _dbContext.ShiftsSchedules.Add(newSchedule);
        await _dbContext.SaveChangesAsync();
    }
}