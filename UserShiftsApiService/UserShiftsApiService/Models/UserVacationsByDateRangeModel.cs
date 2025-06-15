using System;
using System.Text.Json.Serialization;

namespace UserShiftsApiService.Entities;

public class UserVacationsByDateRangeModel
{
    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; }
}
