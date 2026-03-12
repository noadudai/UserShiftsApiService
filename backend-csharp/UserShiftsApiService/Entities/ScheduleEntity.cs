using System;
using System.Collections.Generic;

namespace UserShiftsApiService.Entities;

public class ScheduleEntity
{
    public string Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreatedByManagerId { get; set; }
    public virtual UserEntity Manager { get; set; }
    public ScheduleStatus Status { get; set; }
}