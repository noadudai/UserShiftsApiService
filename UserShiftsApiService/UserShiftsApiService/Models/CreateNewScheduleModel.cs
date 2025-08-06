using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UserShiftsApiService.Models;

public class CreateNewScheduleModel
{
    [JsonPropertyName("shifts")]
    public List<ShiftModel> Shifts { get; set; }
}