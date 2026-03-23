using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserShiftsApiService.Entities;
using UserShiftsApiService.UserContext;

namespace UserShiftsApiService.ActionFilters;

public class RequireManagerDbRoleFilter : IAuthorizationFilter, IOrderedFilter
{
    private readonly IUserContextProvider _userContextProvider;

    public RequireManagerDbRoleFilter(IUserContextProvider userContextProvider)
    {
        _userContextProvider = userContextProvider;
    }

    public int Order => int.MinValue + 1;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userContext = _userContextProvider.GetUserContext();
        if (userContext == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (userContext.Role != UserRole.Manager)
        {
            context.Result = new ForbidResult();
        }
    }
}
