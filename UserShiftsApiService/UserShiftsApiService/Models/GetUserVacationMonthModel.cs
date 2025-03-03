using System;
using System.Text.Json.Serialization;

namespace UserShiftsApiService.Models;

public class GetUserVacationMonthModel
{
    [JsonPropertyName("month_number")]
    public int MonthNumber { get; set; }
    
}