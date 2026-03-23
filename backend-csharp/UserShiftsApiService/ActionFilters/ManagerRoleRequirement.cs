using Microsoft.AspNetCore.Authorization;

namespace UserShiftsApiService.ActionFilters;

public class ManagerRoleRequirement : IAuthorizationRequirement
{
}
