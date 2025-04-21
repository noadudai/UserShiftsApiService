using System;
using System.Text.Json.Serialization;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ShiftModel
{
    [JsonPropertyName("shift_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ShiftType ShiftType { get; set; }
    
    [JsonPropertyName("shift_start_time")]
    public DateTime StartDate { get; set; }
    
    [JsonPropertyName("shift_end_time")]
    public DateTime EndDate { get; set; }
}