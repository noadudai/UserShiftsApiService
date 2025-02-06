using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using UserShiftsApiService.Models;
using UserShiftsApiService.UserContext;

namespace UserShiftsApiService.Middlewares;

public class UserContextProviderMiddleware : IAsyncActionFilter
{
    private readonly IUserContextProvider _contextProvider;
    private readonly ShiftsSchedulingContext _shiftsSchedulingContext;

    public UserContextProviderMiddleware(IUserContextProvider contextProvider, ShiftsSchedulingContext shiftsSchedulingContext)
    {
        _contextProvider = contextProvider;
        _shiftsSchedulingContext = shiftsSchedulingContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var authSub = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(authSub))
        {
            var user = _shiftsSchedulingContext.Users.FirstOrDefault(u => u.AuthSub == authSub);

            if (user != null)
            {
                var userContext = new UserContext.UserContext(){UserId = user.Id};
                _contextProvider.SetUserContext(userContext); 
            }
            else
            {
                throw new KeyNotFoundException("User not found");
            }
        }
        else
        {
            throw new AuthenticationException("Unauthenticated user");
        }
        
        await next();
    }
}