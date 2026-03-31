using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class EmployeeModel
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public UserRole Role { get; set; }
}
