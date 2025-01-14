using System.Text.Json.Serialization;

namespace UserShiftsApiService.Models;

public class Auth0EmployeeModel
{
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
    
    [JsonPropertyName("user_email")]
    public string UserEmail { get; set; }
    
    [JsonPropertyName("host_name")]
    public string HostName { get; set; }
}