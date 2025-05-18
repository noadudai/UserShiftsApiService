using System;
using System.Collections.Generic;

namespace UserShiftsApiService.Entities;

public class ShiftEntity
{
    public string Id { get; set; }
    public ShiftType ShiftType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}