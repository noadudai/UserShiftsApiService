using System;
using System.Text.Json.Serialization;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ShiftModel
{
    public DateTime ShiftStartTime { get; set; }
    public DateTime ShiftEndTime { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ShiftType ShiftType { get; set; }
}