using System.Text.Json.Serialization;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class UserDateRangePreferenceRequestModel
{
    [JsonPropertyName("start_date")]
    public string StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public string EndDate { get; set; }
    
    [JsonPropertyName("request_type")]
    public DateRangeRequestType RequestType { get; set; }
}