using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Middlewares;

public class UserIdMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, ShiftsSchedulingContext dbContext)
    {
        
        var authSub = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(authSub))
        {
            var user = dbContext.Users.FirstOrDefault(u => u.AuthSub == authSub);

            if (user != null)
            {
                httpContext.Items["UserId"] = user.Id;
            }
        }
        await _next(httpContext);
    }
}