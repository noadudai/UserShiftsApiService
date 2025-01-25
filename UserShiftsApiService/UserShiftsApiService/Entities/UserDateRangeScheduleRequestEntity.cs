using System;

namespace UserShiftsApiService.Entities;

public class UserDateRangeScheduleRequestEntity
{
    public string Id { get; set; } 
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public DateRangeRequestType RequestType { get; set; }
    
    public string UserId { get; set; }
    public UserEntity User { get; set; }
}