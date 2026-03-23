using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Services;

public class Auth0UserRoleService : IAuth0UserRoleService
{
    public const string RoleClaimType = "https://UsersShiftsApi/roles";
    public const string ManagerRole = "manager";

    public bool IsManager(ClaimsPrincipal user)
    {
        return GetRoles(user).Contains(ManagerRole, StringComparer.OrdinalIgnoreCase);
    }

    public UserRole GetAppRole(ClaimsPrincipal user)
    {
        return IsManager(user) ? UserRole.Manager : UserRole.Employee;
    }

    private static IEnumerable<string> GetRoles(ClaimsPrincipal user)
    {
        return user.FindAll(RoleClaimType)
            .SelectMany(claim => ParseClaimValue(claim.Value))
            .Where(role => !string.IsNullOrWhiteSpace(role));
    }

    private static IEnumerable<string> ParseClaimValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Array.Empty<string>();
        }

        var trimmedValue = value.Trim();
        if (!trimmedValue.StartsWith("[", StringComparison.Ordinal))
        {
            return new[] { trimmedValue };
        }

        try
        {
            return JsonSerializer.Deserialize<string[]>(trimmedValue) ?? Array.Empty<string>();
        }
        catch (JsonException)
        {
            return Array.Empty<string>();
        }
    }
}
