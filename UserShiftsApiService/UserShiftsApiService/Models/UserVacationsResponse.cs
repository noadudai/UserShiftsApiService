using System.Collections.Generic;

namespace UserShiftsApiService.Models;

public class UserVacationsResponse
{
    public List<OneVacationDateRangeModel> Vacations { get; set; }
}