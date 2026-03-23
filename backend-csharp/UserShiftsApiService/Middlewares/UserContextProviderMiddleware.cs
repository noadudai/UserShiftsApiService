using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;
using UserShiftsApiService.UserContext;

namespace UserShiftsApiService.Middlewares;

public class UserContextProviderMiddleware : IAsyncAuthorizationFilter, IOrderedFilter
{
    private readonly IAuth0UserRoleService _auth0UserRoleService;
    private readonly IUserContextProvider _contextProvider;
    private readonly ShiftsSchedulingContext _shiftsSchedulingContext;
    public int Order => int.MinValue;

    public UserContextProviderMiddleware(
        IAuth0UserRoleService auth0UserRoleService,
        IUserContextProvider contextProvider,
        ShiftsSchedulingContext shiftsSchedulingContext)
    {
        _auth0UserRoleService = auth0UserRoleService;
        _contextProvider = contextProvider;
        _shiftsSchedulingContext = shiftsSchedulingContext;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authSub = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? context.HttpContext.User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(authSub))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var user = await _shiftsSchedulingContext.Users.FirstOrDefaultAsync(u => u.AuthSub == authSub);

        if (user == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var claimRole = _auth0UserRoleService.GetAppRole(context.HttpContext.User);
        if (user.Role != claimRole)
        {
            user.Role = claimRole;
            await _shiftsSchedulingContext.SaveChangesAsync();
        }

        var displayName = context.HttpContext.User.Identity?.Name
            ?? context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value
            ?? context.HttpContext.User.FindFirst("name")?.Value
            ?? user.Email;

        _contextProvider.SetUserContext(new UserContext.UserContext
        {
            UserId = user.Id,
            Email = user.Email,
            DisplayName = displayName,
            Role = user.Role,
        });
    }
}