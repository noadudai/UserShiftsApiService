using System;
using System.Collections.Generic;

namespace UserShiftsApiService.Entities;

public class UserShiftsPreferenceRequestEntity
{
    public string Id { get; set; } 
    public string UserId { get; set; }
    public virtual UserEntity User { get; set; }
    public ShiftRequestType ShiftRequestType { get; set; }
    public virtual ICollection<RequestedShiftEntity> RequestedShifts { get; set; }
}
