using System;
using System.Text.Json.Serialization;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class UserDateRangeScheduleRequestModel
{
    [JsonPropertyName("vacation_start_date")]
    public DateTime VacationStartDate { get; set; }

    [JsonPropertyName("vacation_end_date")]
    public DateTime VacationEndDate { get; set; }
}