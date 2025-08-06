using System.Collections.Generic;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ScheduleResponseModel
{
    public required ScheduleModel Schedule {get; set;}
    public required ShiftModel[] Shifts {get; set;}
}