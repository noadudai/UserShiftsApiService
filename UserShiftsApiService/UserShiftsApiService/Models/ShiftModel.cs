using System;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ShiftModel
{
    public DateTime ShiftStartTime { get; set; }
    public DateTime ShiftEndTime { get; set; }
    public ShiftType ShiftType { get; set; }
}