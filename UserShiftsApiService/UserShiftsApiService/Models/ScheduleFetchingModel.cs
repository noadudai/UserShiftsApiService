using System.Text.Json.Serialization;

namespace UserShiftsApiService.Models;

public class ScheduleFetchingModel
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required Order? CreationTimeOrder { get; set; }
}