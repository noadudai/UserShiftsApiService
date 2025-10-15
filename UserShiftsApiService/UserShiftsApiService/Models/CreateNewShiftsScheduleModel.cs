using System.Collections.Generic;
using System.Text.Json.Serialization;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class CreateNewShiftsScheduleModel
{
    [JsonPropertyName("shifts")]
    public required ShiftModel[] Shifts { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required ScheduleStatus Status { get; set; }
}