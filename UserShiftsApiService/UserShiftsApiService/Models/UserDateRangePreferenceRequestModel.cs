using System;
using System.Text.Json.Serialization;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class UserDateRangePreferenceRequestModel
{
    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; }
    
    [JsonPropertyName("request_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DateRangeRequestType RequestType { get; set; }
}