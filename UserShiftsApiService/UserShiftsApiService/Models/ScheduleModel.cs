using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UserShiftsApiService.Models;

public class ScheduleModel
{
    [JsonPropertyName("shifts")]
    public List<ShiftModel> Shifts { get; set; }
}