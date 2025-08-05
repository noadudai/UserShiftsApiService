using System.Collections.Generic;

namespace UserShiftsApiService.Models;

public class SchedulesAndShiftsResponse
{
    public List<ScheduleAndShiftsModel> SchedulesAndShifts { get; set; }
}