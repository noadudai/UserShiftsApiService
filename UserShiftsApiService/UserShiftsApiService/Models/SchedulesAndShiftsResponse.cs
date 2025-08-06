using System.Collections.Generic;

namespace UserShiftsApiService.Models;

public class SchedulesAndShiftsResponse
{
    public required List<ScheduleResponseModel> SchedulesAndShifts { get; set; }
}