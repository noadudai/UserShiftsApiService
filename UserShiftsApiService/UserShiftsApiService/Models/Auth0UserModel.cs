using System.Text.Json.Serialization;

namespace UserShiftsApiService.Models;

public class Auth0UserModel
{
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
    
    [JsonPropertyName("user_email")]
    public string UserEmail { get; set; }
}