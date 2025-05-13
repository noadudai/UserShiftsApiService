using System;
using System.Collections.Generic;

namespace UserShiftsApiService.Entities;

using noadudai.schedule_generator_client.Model;

public class UserShiftPreferenceRequestEntity
{
    public string Id { get; set; } 
    
    public ShiftRequestType ShiftRequestType { get; set; }
    
    public List<Guid> ShiftIds { get; set; }
    
    public virtual string UserId { get; set; }
    
    public virtual UserEntity User { get; set; }
}
