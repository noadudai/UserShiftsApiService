using System.Collections.Generic;

namespace UserShiftsApiService.Models;

public class UserVacationsResponse
{
    public List<UserVacationModel> Vacations { get; set; }
}