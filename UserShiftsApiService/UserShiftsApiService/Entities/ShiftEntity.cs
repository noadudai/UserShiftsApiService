using System;

namespace UserShiftsApiService.Entities;

public class ShiftEntity
{
    public Guid Id { get; set; }
    
    public ShiftType ShiftType { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
}