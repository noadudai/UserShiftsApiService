using System;
using System.Collections.Generic;
using noadudai.schedule_generator_client.Model;

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

    public ShiftTypesEnum GetScheduleGeneratorClientShiftType()
    {
        if (!Enum.TryParse<ShiftTypesEnum>(ShiftType.ToString(), true, out var result))
            throw new ArgumentException("Invalid Shift Type");
        if (Enum.IsDefined(typeof(ShiftTypesEnum), result))
        {
            return result;
        }
        throw new ArgumentException("Invalid shift type");
    }
}