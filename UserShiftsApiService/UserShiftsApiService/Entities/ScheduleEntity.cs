using System.Collections.Generic;

namespace UserShiftsApiService.Entities;

public class ScheduleEntity
{
    public string Id { get; set; }
    public virtual ICollection<ShiftEntity> ShiftsInSchedule { get; set; }
}