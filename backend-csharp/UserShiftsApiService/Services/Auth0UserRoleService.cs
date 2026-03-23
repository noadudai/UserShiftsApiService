using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Services;

public class Auth0UserRoleService : IAuth0UserRoleService
{
    public const string ManagerRole = "manager";
    private readonly string _roleClaimType;

    public Auth0UserRoleService(IConfiguration configuration)
    {
        var audience = configuration["Auth0:Audience"];
        if (string.IsNullOrWhiteSpace(audience))
        {
            throw new InvalidOperationException("Missing required configuration value: Auth0:Audience");
        }

        _roleClaimType = $"{audience.TrimEnd('/')}/roles";
    }

    public bool IsManager(ClaimsPrincipal user)
    {
        return GetRoles(user).Contains(ManagerRole, StringComparer.OrdinalIgnoreCase);
    }

    public UserRole GetAppRole(ClaimsPrincipal user)
    {
        return IsManager(user) ? UserRole.Manager : UserRole.Employee;
    }

    private ICollection<string> GetRoles(ClaimsPrincipal user)
    {
        var roles = new List<string>();

        foreach (var claim in user.FindAll(_roleClaimType))
        {
            foreach (var role in ParseClaimValue(claim.Value))
            {
                if (!string.IsNullOrWhiteSpace(role))
                {
                    roles.Add(role);
                }
            }
        }

        return roles;
    }

    private static ICollection<string> ParseClaimValue(string value)
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
            return JsonSerializer.Deserialize<string[]>(trimmedValue)
                ?? throw new InvalidOperationException("Auth0 role claim JSON cannot be null.");
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Failed to parse Auth0 role claim JSON.", ex);
        }
    }
}
