using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class CurrentUserResponseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
}
