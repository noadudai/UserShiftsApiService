using System.Collections.Generic;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public record ScheduleModel
{
    public required ScheduleEntity Schedule {get; set;}
    public List<ShiftEntity> Shifts {get; set;}
}