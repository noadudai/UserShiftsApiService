using System.Security.Claims;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Services;

public interface IAuth0UserRoleService
{
    bool IsManager(ClaimsPrincipal user);
    UserRole GetAppRole(ClaimsPrincipal user);
}
