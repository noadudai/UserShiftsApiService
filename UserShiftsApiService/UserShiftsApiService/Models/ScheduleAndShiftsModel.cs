using System.Collections.Generic;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ScheduleAndShiftsModel
{
    public ScheduleEntity Schedule {get; set;}
    public List<ShiftEntity> Shifts {get; set;}
}