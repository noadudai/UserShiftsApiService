using System;
using System.Text.Json.Serialization;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ScheduleModel
{
    public required string Id { get; set; }
    public required DateTime CreationDate { get; set; }
    public required string CreatedByManagerId  { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required ScheduleStatus Status { get; set; }
}