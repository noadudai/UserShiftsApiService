using System.Text.Json.Serialization;

namespace UserShiftsApiService.Models;

public class UserDateRangeScheduleRequestModel
{
    [JsonPropertyName("vacation_start_date")]
    public string VacationStartDate { get; set; }

    [JsonPropertyName("vacation_end_date")]
    public string VacationEndDate { get; set; }
}