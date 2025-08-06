using System.Collections.Generic;

namespace UserShiftsApiService.Models;

public class AllSchedulesResponse
{
    public required ScheduleResponseModel[] Schedules { get; set; }
}