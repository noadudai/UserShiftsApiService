using System.Text.Json.Serialization;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ChangeShiftsScheduleStatusModel
{
    public required string ScheduleId { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required ScheduleStatus Status { get; set; }
}