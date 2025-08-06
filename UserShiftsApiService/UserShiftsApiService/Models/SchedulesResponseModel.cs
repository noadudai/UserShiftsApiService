using System.Collections.Generic;

namespace UserShiftsApiService.Models;

public class SchedulesResponseModel
{
    public required ScheduleResponseModel[] Schedules { get; set; }
}