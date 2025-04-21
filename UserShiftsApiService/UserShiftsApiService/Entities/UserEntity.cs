using System;
using System.Collections.Generic;

namespace UserShiftsApiService.Entities;

public class UserEntity
{
    public string Id { get; set; }
    public string AuthSub { get; set; }
    public string Email { get; set; }
    
    public ICollection<UserDateRangePreferenceRequestEntity> DateRangePreferences { get; set; }
    public ICollection<UserShiftPreferenceRequestEntity> ShiftPreferences { get; set; }
}