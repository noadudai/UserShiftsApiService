using System;
using System.Text.Json.Serialization;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ShiftModel
{
    public required DateTime ShiftStartTime { get; set; }
    public required DateTime ShiftEndTime { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required ShiftType ShiftType { get; set; }
}