using System;
using System.Collections.Generic;
using noadudai.schedule_generator_client.Model;

namespace UserShiftsApiService.Entities;

public class ShiftEntity
{
    public string Id { get; set; }
    public ShiftType ShiftType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<RequestedShiftEntity> UserRequestedShifts { get; set; }
}