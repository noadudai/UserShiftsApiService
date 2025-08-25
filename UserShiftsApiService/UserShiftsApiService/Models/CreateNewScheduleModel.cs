using System.Collections.Generic;
using System.Text.Json.Serialization;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class CreateNewScheduleModel
{
    [JsonPropertyName("shifts")]
    public ShiftModel[] Shifts { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ScheduleStatus Status { get; set; } = ScheduleStatus.Draft;
}