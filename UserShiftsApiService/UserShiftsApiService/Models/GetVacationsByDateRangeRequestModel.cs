using System;
using System.Text.Json.Serialization;

namespace UserShiftsApiService.Models;

public class GetVacationsByDateRangeRequestModel
{
    [JsonPropertyName("start_date")]
    public DateTime StartDateOfRange { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime EndDateOfRange { get; set; }
    
}