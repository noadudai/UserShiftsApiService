using System.Collections.Generic;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public record SchedulModel
{
    public required ScheduleEntity Schedule {get; set;}
    public List<ShiftEntity> Shifts {get; set;}
}