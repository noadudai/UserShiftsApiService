using System;

namespace UserShiftsApiService.Models;

public class UserVacationsByDateRangeModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}