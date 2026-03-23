using UserShiftsApiService.Entities;

namespace UserShiftsApiService.UserContext;

public record UserContext
{
    public string UserId { get; init; }
    public string Email { get; init; }
    public string DisplayName { get; init; }
    public UserRole Role { get; init; }
}