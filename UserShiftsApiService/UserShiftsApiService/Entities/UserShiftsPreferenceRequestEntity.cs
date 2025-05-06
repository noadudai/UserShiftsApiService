using System;
using System.Collections.Generic;

namespace UserShiftsApiService.Entities;

using noadudai.schedule_generator_client.Model;

public class UserShiftsPreferenceRequestEntity
{
    public string Id { get; set; } 
    public virtual string UserId { get; set; }
    public virtual UserEntity User { get; set; }
    public ShiftRequestType ShiftRequestType { get; set; }
    public virtual ICollection<RequestedShiftEntity> RequestedShifts { get; set; }
}
