using System;
using System.Collections.Generic;

namespace UserShiftsApiService.Entities;

public class ShiftEntity
{
    public string Id { get; set; }
    public ShiftType ShiftType { get; set; }
    // Store dates in the database in UTC format
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public string ScheduleId { get; set; }
    public virtual ScheduleEntity Schedule { get; set; }
}