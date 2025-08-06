using System;

namespace UserShiftsApiService.Models;

public class ScheduleModel
{
    public required string Id { get; set; }
    public required DateTime CreationDate { get; set; }
    public required string CreatedByManagerId  { get; set; }
}