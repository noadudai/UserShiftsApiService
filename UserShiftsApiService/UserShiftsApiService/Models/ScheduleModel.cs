using System;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ScheduleModel
{
    public required string Id { get; set; }
    public required DateTime CreationDate { get; set; }
    public required string CreatedByManagerId  { get; set; }
    public required ScheduleStatus Status { get; set; }
}